import { useCallback, useRef, useState, useEffect } from 'react';
import EditorJS from '@editorjs/editorjs';
import { Link } from 'react-router-dom';
import axios from 'axios';
import classNames from 'classnames/bind';

import config from './tools';
import styles from '../CreatePost/CreatePost.module.scss';

const cx = classNames.bind(styles);

function CreatePost() {
    const refEdit = useRef();
    const [editor, seteditor] = useState({});
    const [visible, setVisible] = useState(false);
    const [data, setData] = useState({});
    // const [dataPost, setDataPost] = useState({});
    const [dataPost, setDataPost] = useState({
        title: "Title", 
        description: "Description", 
        category: {
            _id: "1",
            name: "Category" 
        }
    });

    const [content, setContent] = useState('');

    useEffect(() => {
        if (content) {
            const editor = new EditorJS({
                holder: 'editorjs',
                readOnly: false,
                tools: config,
                data: content,
            });
            seteditor(editor);
        }
    }, [content]);

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
        <div clasName={cx('mt-80')}>
            <div clasName={cx('post')}>
                <form action="" method="POST" onSubmit={onSubmit}>
                    <div clasName={cx('post__container')}>
                        <div
                            suppressContentEditableWarning
                            clasName={cx('post__title')}
                            ref={refEdit}
                            value={dataPost.title}
                            onInput={(e) => setData({ ...data, title: e.currentTarget.textContent })}
                        >
                            {dataPost.title}
                        </div>
                        <div clasName={cx('post__content')}>
                            <div id="editorjs" />
                        </div>
                        <div clasName={cx('post__button')}>
                            <button clasName={cx('post__button-main', 'border', 'save')}>Lưu nháp</button>
                            <button clasName={cx('post__button-main', 'border', 'next')} onClick={handleVisibleModal}>
                                Bước tiếp theo
                            </button>
                        </div>
                    </div>
                    {visible && (
                        <div clasName={cx('modal')}>
                            <div clasName={cx('modal__container')}>
                                <div clasName={cx('modal__content')}>
                                    <div clasName={cx('modal__desc')}>
                                        <p clasName={cx('modal__title')}>
                                            Mô tả bài viết
                                            <em clasName={cx('modal__title-sub')}> (không bắt buộc)</em>
                                        </p>
                                        <textarea
                                            clasName={cx('modal__desc-input')}
                                            onChange={(e) => setData({ ...data, description: e.target.value })}
                                        >
                                            {dataPost.description}
                                        </textarea>
                                    </div>
                                    <div clasName={cx('modal__tagname')}>
                                        <p clasName={cx('modal__title')}>
                                            Thêm thẻ tag
                                            <em clasName={cx('modal__title-sub')}> (tối đa 5 thẻ)</em>
                                        </p>
                                        <div clasName={cx('modal__tagname-container')}>
                                            <div clasName={cx('tagname__search')}>
                                                <i clasName={cx('tagname__search-icon', 'bx', 'bx-search')}></i>
                                                <input
                                                    type="text"
                                                    clasName={cx('tagname__search-input')}
                                                    placeholder="Tìm thẻ tag..."
                                                />
                                            </div>
                                            <div clasName={cx('tagname__selected')}>
                                                <div clasName={cx('tagname__selected-container')}>
                                                    <span clasName={cx('tagname__selected-content', 'name')}>
                                                        KHOA HỌC
                                                    </span>
                                                    <span clasName={cx('tagname__selected-content', 'quantity')}>
                                                        1102
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div clasName={cx('modal__category')}>
                                        <p clasName={cx('modal__title')}>Chọn danh mục</p>
                                        <div clasName={cx('modal__category-container')}>
                                            <select
                                                id="selected-id"
                                                clasName={cx('modal__category-select')}
                                                onChange={(e) => setData({ ...data, category: e.target.value })}
                                            >
                                                <option
                                                    clasName={cx('modal__category-option')}
                                                    value={dataPost.category._id}
                                                    key={dataPost.category._id}
                                                >
                                                    {dataPost.category.name}
                                                </option>
                                                {/* {categorise.data.map((e, i) => (
                                                    <option
                                                        value={e._id}
                                                        key={e._id}
                                                        clasName={cx('modal__category-option')}
                                                    >
                                                        {e.name}
                                                    </option>
                                                ))} */}
                                            </select>
                                            <div clasName={cx('modal__category-icon')}>
                                                <i
                                                    clasName={cx('modal__category-icon-down', 'bx', 'bxs-chevron-down')}
                                                ></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div clasName={cx('modal__button')}>
                                        <button
                                            clasName={cx('modal__button-content', 'back')}
                                            onClick={handleVisibleModal}
                                        >
                                            Quay lại
                                        </button>
                                        <Link to="/">
                                            <button
                                                onClick={onSave}
                                                type="submit"
                                                clasName={cx('modal__button-content', 'create')}
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
        </div>
    );
}

export default CreatePost;
