import { useCallback, useRef, useState, useEffect } from 'react';
import EditorJS from '@editorjs/editorjs';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown } from '@fortawesome/free-solid-svg-icons';
import classNames from 'classnames/bind';

import { Config } from './tools';
import styles from '../CreatePost/CreatePost.module.scss';

const cx = classNames.bind(styles);

function EditPost() {
    const refEdit = useRef();
    const [editor, setEditor] = useState({});
    const [visible, setVisible] = useState(false);
    const [data, setData] = useState({});
    const [content, setContent] = useState(null);
    const [isEditorInitialized, setIsEditorInitialized] = useState(false);

    // const [dataPost, setDataPost] = useState({});
    const [dataPost, setDataPost] = useState({
        title: 'Title',
        description: 'Description',
        category: {
            _id: '1',
            name: 'Category',
        },
    });

    useEffect(() => {
        const postData = {
            blocks: [
                {
                    type: 'header',
                    data: {
                        text: 'Your header text here',
                        level: 2,
                    },
                },
                {
                    type: 'image',
                    data: {
                        file: {
                            url: 'https://images.spiderum.com/sp-images/6551d740e46b11eeb07a0149184cedb1.png',
                        },
                        caption: 'Caption 1',
                        withBorder: false,
                        withBackground: false,
                        stretched: false,
                    },
                },
                {
                    type: 'paragraph',
                    data: {
                        text: 'Your post content here',
                    },
                },
                {
                    type: 'image',
                    data: {
                        file: {
                            url: 'https://images.spiderum.com/sp-images/0fe61000efec11ee8536970f721d609f.png',
                        },
                        caption: 'Caption 2',
                        withBorder: false,
                        withBackground: false,
                        stretched: false,
                    },
                },
            ],
        };

        setContent(postData);
    }, []);

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
                editorContainer.removeChild(editorContainer.children[1]);
            }
        }
    }, [isEditorInitialized]);

    const handleVisibleModal = useCallback((e) => {
        e.preventDefault();
        setVisible(!visible);
    });

    const onSubmit = useCallback((e) => {
        e.preventDefault();
    }, []);

    const onSave = useCallback(async (e) => {
        e.preventDefault();
    });

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
                                            onChange={(e) => setData({ ...data, category: e.target.value })}
                                        >
                                            {/* <option
                                                    className={cx('modal__category-option')}
                                                    value={dataPost.category._id}
                                                    key={dataPost.category._id}
                                                >
                                                    {dataPost.category.name}
                                                </option> */}
                                            <option value={1} key={1} className={cx('modal__category-option')}>
                                                Thể thao
                                            </option>
                                            {/* {categorise.data.map((e, i) => (
                                                    <option
                                                        value={e._id}
                                                        key={e._id}
                                                        className={cx('modal__category-option')}
                                                    >
                                                        {e.name}
                                                    </option>
                                                ))} */}
                                            <option value={2} key={2} className={cx('modal__category-option')}>
                                                Chính trị
                                            </option>
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
                                    <Link to="/">
                                        <button
                                            onClick={onSave}
                                            type="submit"
                                            className={cx('modal__button-content', 'create')}
                                        >
                                            Cập nhật
                                        </button>
                                    </Link>
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
