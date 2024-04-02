import React, { useState, useEffect, useRef, useCallback } from 'react';
import { Link } from 'react-router-dom';
import EditorJS from '@editorjs/editorjs';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown } from '@fortawesome/free-solid-svg-icons';

import config from './tools.js';
import styles from './CreatePost.module.scss';

const cx = classNames.bind(styles);

function CreatePost() {
    const refEdit = useRef();
    const toast = useRef(null);

    const [isEditorInitialized, setIsEditorInitialized] = useState(false);

    const [editor, setEditor] = useState(null);
    const [error, setError] = useState(null);
    const [visible, setVisible] = useState(false);

    const [data, setData] = useState({
        title: '',
        content: '',
        description: '',
        category: '123a123',
    });

    useEffect(() => {
        window.scrollTo({
            top: 0,
            left: 0,
        });
    }, []);

    const onSubmit = useCallback((e) => {
        e.preventDefault();
    }, []);

    useEffect(() => {
        const editorInstance = new EditorJS(config());
        setEditor(editorInstance);

        setIsEditorInitialized(true);
    }, []);

    useEffect(() => {
        if (isEditorInitialized) {
            const editorContainer = document.getElementById('editorjs');

            if (editorContainer && editorContainer.children.length >= 2) {
                editorContainer.removeChild(editorContainer.children[1]);
            }
        }
    }, [isEditorInitialized]);

    const handleVisibleModal = useCallback(
        (e) => {
            setError(null);
            e.preventDefault();

            setVisible(!visible);
        },
        [visible, editor, data],
    );

    const onSave = useCallback(
        (e) => {
            e.preventDefault();
        },
        [data],
    );

    useEffect(() => {
        document.title = 'Viết bài mới...';
    }, []);

    return (
        <div>
            <div className={cx('post')}>
                <form action="" method="POST" onSubmit={onSubmit}>
                    <div className={cx('post__container')}>
                        <div
                            suppressContentEditableWarning
                            placeholder="Tiêu đề bài viết......."
                            className={cx('post__title')}
                            ref={refEdit}
                            value={data.title}
                            onInput={(e) => setData({ ...data, title: e.currentTarget.textContent })}
                        ></div>
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
                                {error ? (
                                    <div className={cx('toast-mess-container')}>
                                        <button ref={toast} className={cx('alert-toast-message', 'err')}>
                                            {error}
                                        </button>
                                    </div>
                                ) : (
                                    ''
                                )}
                                <div className={cx('modal__content')}>
                                    <div className={cx('modal__desc')}>
                                        <p className={cx('modal__title')}>
                                            Mô tả bài viết
                                            <em className={cx('modal__title-sub')}> (không bắt buộc)</em>
                                        </p>
                                        <textarea
                                            className={cx('modal__desc-input')}
                                            value={data.description}
                                            onChange={(e) => setData({ ...data, description: e.target.value })}
                                        ></textarea>
                                    </div>
                                    <div className={cx('modal__category')}>
                                        <p className={cx('modal__title')}>Chọn danh mục</p>
                                        <div className={cx('modal__category-container')}>
                                            <select
                                                id="selected-id"
                                                className={cx('modal__category-select')}
                                                onChange={(e) => setData({ ...data, category: e.target.value })}
                                            >
                                                <option className={cx('modal__category-option')}>
                                                    -- Chọn danh mục --
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
                                                <option value={1} key={1} className={cx('modal__category-option')}>
                                                    Thể thao
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
                                                Tạo
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
