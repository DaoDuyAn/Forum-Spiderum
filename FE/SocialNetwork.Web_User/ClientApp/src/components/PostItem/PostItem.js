import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faCommentDots,
    faHeart as regularHeart,
    faBookmark as regularBookmark,
} from '@fortawesome/free-regular-svg-icons';
import { faHeart as solidHeart, faBookmark as solidBookmark } from '@fortawesome/free-solid-svg-icons';

import styles from './PostItem.module.scss';

const cx = classNames.bind(styles);

function PostItem({ post }) {

    const [isBookmark, setIsBookmark] = useState(false);
    const [isLike, setLike] = useState(false);

    const handleSavePost = (e, id) => {
        setIsBookmark(!isBookmark);
    };

    const handleLike = (e, id) => {
        setLike(!isLike);
    };

    return (
        <div className={cx('row', 'mb', 'post')}>
            <div className={cx('lg:w-1/3', 'sm:w-full')}>
                <div className={cx('filter__content-img')}>
                    <Link to={`/post/${post.postInfo.slug}`} className={cx('filter__content-img')}>
                        <img
                            src={
                                post.postInfo.thumbnailImagePath !== ''
                                    ? post.postInfo.thumbnailImagePath
                                    : 'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                            }
                            alt="Ảnh của bài viết"
                            className={cx('filter__content-img-item')}
                        />
                    </Link>
                </div>
            </div>
            <div className={cx('lg:w-2/3', 'sm:w-full')}>
                <div className={cx('filter__content-container')}>
                    <div className={cx('filter__content-heading')}>
                        <div>
                            <Link to={`/category/${post.postCategoryInfo.slug}`}>
                                <span className={cx('title-category')}>{post.postCategoryInfo.categoryName}</span>
                            </Link>
                        </div>
                        <Link
                            to={`/`}
                            onClick={(e) => {
                                handleSavePost(e, post.postInfo.id);
                            }}
                            className={cx('post_saved')}
                            title="Lưu bài viết"
                        >
                            {isBookmark ? (
                                <FontAwesomeIcon className={cx('save')} icon={solidBookmark} />
                            ) : (
                                <FontAwesomeIcon icon={regularBookmark} />
                            )}
                        </Link>
                    </div>
                    <div className={cx('filter__content-main')}>
                        <Link to={`/post/${post.postInfo.slug}`}>
                            <h3 className={cx('title-post')}>
                                {post.postInfo.title}
                            </h3>
                        </Link>
                        <div className={cx('')}>
                            <p className={cx('text-intro')}>
                                {post.postInfo.description}
                            </p>
                        </div>
                    </div>
                    <div className={cx('filter__content-author')}>
                        <div className={cx('filter__content-author-user')}>
                            <div className={cx('post-avt')}>
                                <Link to={`/user/{post.userInfo.userName}`}>
                                    <img
                                        className={cx('post-avt')}
                                        src={post.userInfo.avatarImagePath !== "" ? post.userInfo.avatarImagePath : 'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'}
                                        alt="ava"
                                    />
                                </Link>
                            </div>
                            <div className={cx('post-author')}>
                                <Link to={`/user/${post.userInfo.userName}`}>
                                    <p className={cx('post-username')}>{post.userInfo.fullName}</p>
                                </Link>
                                <span className={cx('time-read')}>- {post.postInfo.creationDate}</span>
                            </div>
                        </div>
                        <div className={cx('filter__content-interactive')}>
                            <div
                                className={cx('filter__content-interactive-container')}
                                title="Thích"
                                onClick={(e) => handleLike(e, post.postInfo.id)}
                            >
                                {isLike ? (
                                    <FontAwesomeIcon className={cx('like')} icon={solidHeart} />
                                ) : (
                                    <FontAwesomeIcon icon={regularHeart} />
                                )}
                                <span className={cx('post-icon')}>{post.postInfo.likesCount}</span>
                            </div>
                            <div className={cx('filter__content-interactive-container')} title="Bình luận">
                                <FontAwesomeIcon icon={faCommentDots} />
                                <span className={cx('post-icon')}>{post.postInfo.commentsCount}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PostItem;
