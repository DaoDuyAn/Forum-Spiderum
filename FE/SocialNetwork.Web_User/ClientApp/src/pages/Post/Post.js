import { useState, useRef, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart as regularHeart, faBookmark as regularBookmark } from '@fortawesome/free-regular-svg-icons';
import { faHeart as solidHeart, faBookmark as solidBookmark, faComments } from '@fortawesome/free-solid-svg-icons';
import Slider from 'react-slick';
import classNames from 'classnames/bind';

import Comment from '~/components/Comment';
import TrendingPosts from '~/components/TrendingPosts';
import styles from './Post.module.scss';

const cx = classNames.bind(styles);

function Post() {
    const inputCmtRef = useRef(null);
    const toast = useRef(null);

    const [posts, setPosts] = useState(null);
    const [isUser, setIsUser] = useState(true);
    const [active, setActive] = useState(true);
    const [authPost, setAuthPost] = useState({});
    const [isSuccess, setIsSuccess] = useState(null);
    const [visiable, setVisiable] = useState(false);
    // const currentUser =
    const [activeCate, setActiveCate] = useState(false);
    const [isBookmark, setIsBookmark] = useState(true);
    const [isLike, setLike] = useState(true);
    const [likeCount, setLikeCount] = useState(100);

    const [dataComment, setDataComment] = useState({});
    const [comments, setComments] = useState([]);

    const sliderSettings = {
        slidesToShow: 3,
        slidesToScroll: 1,
        infinite: false,
        speed: 500,
        lazyLoad: true,
    };

    useEffect(() => {
        window.scrollTo({
            top: 0,
            left: 0,
        });
    }, []);


    const handleClickDelete = () => setVisiable(!visiable);

    const handleSavePost = (e) => {
        setIsBookmark(false);
    };

    const handleLike = (e) => {
        setLike(!isLike);
    };

    const handleFollow = (e) => {};

    const handleUnFollow = (e) => {};

    const handleFollowCategory = (e) => {};

    const handleUnFollowCategory = (e) => {};

    const handleSubmitComment = (e) => {};

    const handleDelete = (e) => {};

    return (
        <div>
            <div className={cx('post__details-container')}>
                {isSuccess ? (
                    <div className={cx('toast-mess-container')}>
                        <button ref={toast} className={cx('alert-toast-message', 'success')}>
                            {isSuccess}
                        </button>
                    </div>
                ) : (
                    <></>
                )}

                <div className={cx('post__details-auth')}>
                    <div className={cx('post__details-category')}>
                        <Link to={`/category/a`}>
                            <span className={cx('post__details-category-name')}>Khoa học - Công nghệ</span>
                        </Link>
                    </div>
                    <div className={cx('post__details-title')}>
                        <h1>FACEBOOK SẬP, KỸ SƯ FACEBOOK LÀM GÌ?</h1>
                    </div>
                    <div className={cx('post__details-desc')}>
                        <p>
                            Calvin đang ngồi trên xe bus để tới văn phòng. Hôm nay Menlo Park se se lạnh và âm u. Một
                            ngày thứ 3 buồn tẻ chẳng khác gì thời tiết...
                        </p>
                    </div>
                    <div className={cx('post__profile')}>
                        <div className={cx('flex')}>
                            <div className={cx('post-avt')}>
                                <Link to={`/user/an`}>
                                    <img
                                        src={
                                            'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                        }
                                        alt=""
                                    />
                                </Link>
                            </div>
                            <div className={cx('post-info')}>
                                <Link to={`/user/an`}>
                                    <p className={cx('post-username')}>DaoDuyAn</p>
                                </Link>
                                <div>
                                    <p className={cx('post-date-created')}>8/8/2024</p>
                                </div>
                            </div>
                        </div>
                        {isUser ? (
                            <div className={cx('btn-user')}>
                                <Link to={`/post/edit/bai-viet`}>
                                    <span className={cx('button-data', 'edit')}>Sửa</span>
                                </Link>
                                <button className={cx('btn-delete')} onClick={handleClickDelete}>
                                    <span className={cx('button-data', 'delete')}>Xóa</span>
                                </button>
                            </div>
                        ) : (
                            ''
                        )}
                    </div>
                </div>

                <div className={cx('post__details-content')}>
                    <div className={cx('post__details-content-container')}>
                        Một keyword xuyên suốt trong tất cả các thành công đó là sự kỷ luật. Self-discipline - Kỷ luật
                        bản thân là khả năng thúc đẩy bản thân tiến về phía trước, duy trì động lực và hành động, bất kể
                        bạn đang cảm thấy thế nào, về thể chất hay tinh thần. Nghiên cứu khoa học chỉ ra rằng kỷ luật
                        bản thân giúp bạn thành công trong học tập và cuộc sống. Một lý do chính khiến cho các bạn tuổi
                        vị thành niên mất đi tiềm năng trí tuệ là do họ không rèn được tính kỷ luật tự giác.
                    </div>
                </div>

                <div className={cx('post__tool-bar')}>
                    <div className={cx('pull-left')}>
                        <div className={cx('vote')}>
                            <div className={cx('upvote')} onClick={handleLike}>
                                <div>
                                    {isLike ? (
                                        <FontAwesomeIcon className={cx('like')} icon={solidHeart} />
                                    ) : (
                                        <FontAwesomeIcon icon={regularHeart} />
                                    )}
                                </div>
                            </div>
                            <span className={cx('value')}>{likeCount} lượt thích</span>
                        </div>
                    </div>
                    <div className={cx('pull-right')}>
                        <div className={cx('right-tools')}>
                            <div className={cx('bookmark')}>
                                <Link to="/" title="Lưu bài viết">
                                    {isBookmark ? (
                                        <FontAwesomeIcon className={cx('save')} icon={solidBookmark} />
                                    ) : (
                                        <FontAwesomeIcon icon={regularBookmark} />
                                    )}
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>

                <div className={cx('post__subscription')}>
                    <div className={cx('post__author')}>
                        <div className={cx('post__author-container')}>
                            <div className={cx('post__author-infos')}>
                                <div className={cx('post__author-avt')}>
                                    <Link to={`/user/an`}>
                                        <img
                                            src={
                                                'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                            }
                                            alt="ava_img"
                                        />
                                    </Link>
                                </div>
                                <div className={cx('name')}>
                                    <Link to={`/user/an`} className={cx('name-main')}>
                                        Duy An
                                    </Link>
                                    <p className={cx('post__slug')}>@daoduyan</p>
                                </div>
                            </div>
                            <div className={cx('sub-container')}>
                                {/* {currentUser.currentUser?._id === authPost?._id ? (
                                    ''
                                ) :  */}
                                {active ? (
                                    <button className={cx('btn-fl', 'followed')} onClick={handleUnFollow}>
                                        Đang theo dõi
                                    </button>
                                ) : (
                                    <button className={cx('btn-fl', 'follow')} onClick={handleFollow}>
                                        Theo dõi
                                    </button>
                                )}
                            </div>
                        </div>
                        <div className={cx('user-desc')}>
                            Sự cuộn sóng trong dòng chữ phản ánh đại dương nơi tâm hồn
                        </div>
                    </div>
                    <div className={cx('category__item')}>
                        <div className={cx('catergory__info')}>
                            <Link className={cx('name-main')} to={`/category/a`}>
                                <span>Khoa học - Công nghệ</span>
                            </Link>
                            <p className={cx('post__slug')}>/khoa-hoc-cong-nghe</p>
                        </div>
                        {activeCate ? (
                            <button className={cx('btn-fl', 'followed')} onClick={handleUnFollowCategory}>
                                Đang theo dõi
                            </button>
                        ) : (
                            <button className={cx('btn-fl', 'follow')} onClick={handleFollowCategory}>
                                Theo dõi
                            </button>
                        )}
                    </div>
                </div>

                <div className={cx('user__profile')}>
                    <div className={cx('user__profile-posts')}>
                        <div className={cx('user__profile-posts-top')}>
                            <div className={cx('user__profile-posts-head')}>
                                <div className={cx('user__profile-posts-heading')}>
                                    <span>Bài viết nổi bật khác</span>
                                </div>
                            </div>
                            <div className={cx('p-[8px]')}>
                                <TrendingPosts posts={posts?.posts} slice={5} slidesToShow={3} />
                            </div>
                        </div>
                    </div>
                </div>

                <div className={cx('comment__container')}>
                    <section className={cx('comment__section')}>
                        <div>
                            <div className={cx('comment__form-container')}>
                                <form className={cx('comment__form')} ref={inputCmtRef}>
                                    <input
                                        className={cx('comment__form-data')}
                                        ref={inputCmtRef}
                                        placeholder="Hãy chia sẻ cảm nghĩ của bạn về bài viết"
                                        value={dataComment.content}
                                        onChange={(e) => setDataComment({ ...dataComment, content: e.target.value })}
                                    ></input>
                                    <div className={cx('comment__form-actions')} onClick={handleSubmitComment}>
                                        <div className={cx('comment__form-actions-submit')}>Gửi</div>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div className={cx('comment__nav-tab')}>
                            <div className={cx('separator')}></div>
                            <ul className={cx('comment__nav-list')}>
                                <li className={cx('comment__nav-item', 'active')}>
                                    <Link to="/">Hot nhất</Link>
                                </li>
                                <li className={cx('comment__nav-item')}>
                                    <Link to="/">Mới nhất</Link>
                                </li>
                            </ul>
                        </div>
                        <div className={cx('comment__tree-view')}>
                            <div className={cx('comments')}>
                                <div className={cx('comments__node')}>
                                    {comments.length > 0 ? (
                                        comments.map((comment) => (
                                            <Comment postId={1} comment={comment} key={comment._id} />
                                        ))
                                    ) : (
                                        // <div className={cx('no-comment')}>
                                        //     <FontAwesomeIcon className={cx('no-comment-icon')} icon={faComments} />
                                        //     <div className={cx('no-comment-title')}>
                                        //         Hãy là người đầu tiên bình luận bài viết này
                                        //     </div>
                                        // </div>
                                        <>
                                            <Comment postId={1} comment={'d'} key={5} />
                                            <Comment postId={1} comment={'d'} key={2} />
                                        </>
                                    )}
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
            {visiable ? (
                <div className={cx('modal__delete')}>
                    <div className={cx('modal__delete-container')}>
                        <div className={cx('modal__delete-main')}>
                            <header className={cx('modal__delete-header')}>Xóa bài viết</header>
                            <main className={cx('modal__delete-content')}>
                                Sẽ không có cách nào hoàn tác lại hành động này. Bạn có chắc chắn muốn xóa bài viết?
                            </main>
                            <footer className={cx('modal__delete-footer')}>
                            <button onClick={handleClickDelete} className={cx('modal__delete-button', 'cancel')}>
                                    Hủy
                                </button>
                                <button onClick={handleDelete} className={cx('modal__delete-button', 'delete')}>
                                    XÓA
                                </button>
                            </footer>
                        </div>
                    </div>
                </div>
            ) : (
                ''
            )}
        </div>
    );
}

export default Post;
