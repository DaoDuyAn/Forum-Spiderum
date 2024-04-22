import { useCallback, useRef, useState, useEffect } from 'react';
import axios from 'axios';
import EditorJS from '@editorjs/editorjs';
import { Link, useParams, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown } from '@fortawesome/free-solid-svg-icons';
import classNames from 'classnames/bind';

import { Config } from './tools';
import styles from '../CreatePost/CreatePost.module.scss';

const cx = classNames.bind(styles);

function EditPost() {
    const { slug } = useParams();
    const navigate = useNavigate();

    const refEdit = useRef();

    const [editor, setEditor] = useState({});
    const [categories, setCategories] = useState([]);
    const [visible, setVisible] = useState(false);
    const [data, setData] = useState({});
    const [content, setContent] = useState(null);
    const [isEditorInitialized, setIsEditorInitialized] = useState(false);

    // const [dataPost, setDataPost] = useState({
    //     title: '',
    //     content: '',
    //     description: '',
    //     categoryId: '',
    // });
    const [dataPost, setDataPost] = useState({});

    useEffect(() => {
        const fetchPost = async () => {
            try {
                const response = await axios.get(`https://localhost:44379/api/v1/GetPostBySlug/slug/${slug}`);
                const data = response.data;

                setDataPost(data.postInfo);
                setData({
                    title: data.postInfo.title,
                    content: data.postInfo.content,
                    description: data.postInfo.description,
                    id: data.postInfo.id,
                    userId: data.userInfo.id,
                    categoryId: data.postCategoryInfo.id
                });
                setContent(JSON.parse(data.postInfo.content));
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPost();
    }, []);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get(`https://localhost:44379/api/v1/GetAllCategories`);
                const data = response.data;

                setCategories(data);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchCategories();
    }, []);

    // useEffect(() => {
    //     const postData = {
    //         blocks: [
    //             {
    //                 type: 'header',
    //                 data: {
    //                     text: '1, Những thứ mình cần',
    //                     level: 1,
    //                 },
    //             },
    //             {
    //                 type: 'image',
    //                 data: {
    //                     file: {
    //                         url: 'https://images.spiderum.com/sp-images/8390ed30807b11eeb970ebf53e81c32f.jpeg',
    //                     },
    //                     caption: 'Cre: Pinterest',
    //                     withBorder: false,
    //                     withBackground: false,
    //                     stretched: true,
    //                 },
    //             },
    //             {
    //                 type: 'paragraph',
    //                 data: {
    //                     text: '- Đương nhiên đầu tiên là một chiếc xe đạp rồi ^^. Hmm có đa dạng thể loại mà bạn có thể chọn mua với phân khúc giá khác nhau, tùy vào nhu cầu và tài chính của bạn. Mình thì chọn xe tầm 5 triệu là phù hợp với mình. ',
    //                 },
    //             },
    //             {
    //                 type: 'image',
    //                 data: {
    //                     file: {
    //                         url: 'https://images.spiderum.com/sp-images/0fe61000efec11ee8536970f721d609f.png',
    //                     },
    //                     caption: 'Caption 2',
    //                     withBorder: false,
    //                     withBackground: false,
    //                     stretched: true,
    //                 },
    //             },
    //             {
    //                 type: 'image',
    //                 data: {
    //                     file: {
    //                         url: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=',
    //                     },
    //                     caption: 'Caption 3',
    //                     withBorder: false,
    //                     withBackground: false,
    //                     stretched: true,
    //                 },
    //             },
    //         ],
    //     };

    //     setContent(postData);
    // }, []);

    useEffect(() => {
        if (content) {
            const editor = new EditorJS({
                holder: 'editorjs',
                placeholder: 'Nội dung bài viết',
                readOnly: false,
                tools: Config,
                data: content,
            });

            setEditor(editor);
            setIsEditorInitialized(false);
        }

        setIsEditorInitialized(true);
    }, [content]);

    useEffect(() => {
        if (isEditorInitialized) {
            const editorContainer = document.getElementById('editorjs');

            if (editorContainer && editorContainer.children.length >= 2) {
                editorContainer.removeChild(editorContainer.children[0]);
            }
        }
    }, [isEditorInitialized]);

    const handleVisibleModal = useCallback(
        (e) => {
            e.preventDefault();

            if (editor) {
                editor
                    .save()
                    .then((outputData) => {
                        setData({ ...data, content: JSON.stringify(outputData) });
                        setVisible(!visible);
                    })
                    .catch((error) => {
                        console.log('Saving failed: ', error);
                    });
            }
        },
        [visible, editor, data],
    );

    const onSubmit = useCallback((e) => {
        e.preventDefault();
    }, []);

    const onSave = (e) => {
        e.preventDefault();

        const payload = {
            ...data,
        };

        console.log(payload.content);

        
        axios
            .put('https://localhost:44379/api/v1/UpdatePost', payload)
            .then((response) => {
                console.log(response.data);
                navigate(`/post/${response.data}`);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    return (
        <div className={cx('post')}>
            <form action="" method="POST" onSubmit={onSubmit}>
                <div className={cx('post__container')}>
                    <div
                        suppressContentEditableWarning
                        className={cx('post__title')}
                        ref={refEdit}
                        value={dataPost.title}
                        onInput={(e) => setData({ ...data, title: e.currentTarget.textContent })}
                    >
                        {dataPost.title}
                    </div>
                    <div className={cx('post__content')}>
                        <div id="editorjs" />
                    </div>
                    <div className={cx('post__button')}>
                        <button className={cx('post__button-main', 'border', 'next')} onClick={handleVisibleModal}>
                            Bước tiếp theo
                        </button>
                    </div>
                </div>
                {visible && (
                    <div className={cx('modal')}>
                        <div className={cx('modal__container')}>
                            <div className={cx('modal__content')}>
                                <div className={cx('modal__desc')}>
                                    <p className={cx('modal__title')}>
                                        Mô tả bài viết
                                        <em className={cx('modal__title-sub')}> (không bắt buộc)</em>
                                    </p>
                                    <textarea
                                        className={cx('modal__desc-input')}
                                        onChange={(e) => setData({ ...data, description: e.target.value })}
                                    >
                                        {dataPost.description}
                                    </textarea>
                                </div>

                                <div className={cx('modal__category')}>
                                    <p className={cx('modal__title')}>Chọn danh mục</p>
                                    <div className={cx('modal__category-container')}>
                                        <select
                                            id="selected-id"
                                            className={cx('modal__category-select')}
                                            onChange={(e) => setData({ ...data, categoryId: e.target.value })}
                                        >
                                            {categories.map((e, i) => (
                                                <option
                                                    value={e.id}
                                                    key={e.id}
                                                    className={cx('modal__category-option')}
                                                    selected={e.id === data.categoryId ? 'selected' : ''}
                                                >
                                                    {e.categoryName}
                                                </option>
                                            ))}
                                        </select>
                                        <div className={cx('modal__category-icon')}>
                                            <FontAwesomeIcon
                                                icon={faChevronDown}
                                                className={cx('modal__category-icon-down')}
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className={cx('modal__button')}>
                                    <button
                                        className={cx('modal__button-content', 'back')}
                                        onClick={handleVisibleModal}
                                    >
                                        Quay lại
                                    </button>
                                    <button
                                        onClick={onSave}
                                        type="submit"
                                        className={cx('modal__button-content', 'create')}
                                    >
                                        Cập nhật
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </form>
        </div>
    );
}

export default EditPost;
