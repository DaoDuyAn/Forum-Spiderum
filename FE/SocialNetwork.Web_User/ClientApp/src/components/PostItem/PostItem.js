import { useState } from 'react';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faCommentDots,
    faHeart as regularHeart,
    faBookmark as regularBookmark,
} from '@fortawesome/free-regular-svg-icons';
import { faHeart as solidHeart, faBookmark as solidBookmark } from '@fortawesome/free-solid-svg-icons';

import DatePost from '../DatePost';
import styles from './PostItem.module.scss';

const cx = classNames.bind(styles);

function PostItem({ post }) {
    const d = new Date();

    const [isBookmark, setIsBookmark] = useState(true);
    const [isLike, setLike] = useState(true);

    const handleSavePost = (e, id) => {
        setIsBookmark(false);
    };

    const handleLike = (e, id) => {
        setLike(!isLike);
    };

    return (
        <div className={cx('row', 'mb', 'post')}>
            <div className={cx('lg:w-1/3', 'sm:w-full')}>
                <div className={cx('filter__content-img')}>
                    <Link to={`/post/baiviet`} className={cx('filter__content-img')}>
                        <img
                            src={
                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
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
                            <Link to={`/category/phat-trien-ban-than`}>
                                <span className={cx('title-category')}>PHÁT TRIỂN BẢN THÂN</span>
                            </Link>
                        </div>
                        <Link
                            to={`/`}
                            onClick={(e) => {
                                handleSavePost(e, post._id);
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
                        <Link to={`/post/baiviet2`}>
                            <h3 className={cx('title-post')}>
                                Đánh giá Dune phần Đánh giá Dune phần 1Đánh giá Dune phần 1Đánh giá Dune phần 1Đánh giá
                                Dune phần 11
                            </h3>
                        </Link>
                        <div className={cx('')}>
                            <p className={cx('text-intro')}>
                                Nhận xét về một bộ phim đang hot gần đây là Dune Nhận xNhận xNhận xét về một bộ phim
                                đang hot gần đây là Duneét về một bộ phim đang hot gần đây là Duneét về một bộ phim đang
                                hot gần đây là Dune
                            </p>
                        </div>
                    </div>
                    <div className={cx('filter__content-author')}>
                        <div className={cx('filter__content-author-user')}>
                            <div className={cx('post-avt')}>
                                <Link to={`/user/duyan`}>
                                    <img
                                        className={cx('post-avt')}
                                        src={
                                            'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                        }
                                        alt="ava"
                                    />
                                </Link>
                            </div>
                            <div className={cx('post-author')}>
                                <Link to={`/user/duyan`}>
                                    <p className={cx('post-username')}>Đào Duy An</p>
                                </Link>
                                <DatePost date={d}></DatePost>
                            </div>
                        </div>
                        <div className={cx('filter__content-interactive')}>
                            <div
                                className={cx('filter__content-interactive-container')}
                                title="Thích"
                                onClick={(e) => handleLike(e, post._id)}
                            >
                                {isLike ? (
                                    <FontAwesomeIcon className={cx('like')} icon={solidHeart} />
                                ) : (
                                    <FontAwesomeIcon icon={regularHeart} />
                                )}
                                <span className={cx('post-icon')}>123</span>
                            </div>
                            <div className={cx('filter__content-interactive-container')} title="Bình luận">
                                <FontAwesomeIcon icon={faCommentDots} />
                                <span className={cx('post-icon')}>3</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PostItem;
