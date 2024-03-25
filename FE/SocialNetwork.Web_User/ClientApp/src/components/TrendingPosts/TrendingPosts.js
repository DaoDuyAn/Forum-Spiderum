import Slider from 'react-slick';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleChevronRight, faCircleChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';

import styles from './TrendingPosts.module.scss';

const cx = classNames.bind(styles);

function TrendingPosts({ posts, slidesToShow, slice }) {
    function SampleNextArrow(props) {
        const { className, style, onClick } = props;
        return (
            <div
                className={className}
                style={{
                    ...style,
                    display: 'block',
                    background: 'gray',
                    paddingRight: '10px',
                    paddingTop: '1.85px',
                    borderRadius: '50%',
                    boxShadow: '0px 2px 4px rgba(0, 0, 0, 0.2)',
                    transform: 'scale(1.2)',
                    marginLeft: '10px',
                    marginTop: '-20px',
                }}
                onClick={onClick}
            />
        );
    }

    function SamplePrevArrow(props) {
        const { className, style, onClick } = props;
        return (
            <div
                className={className}
                style={{
                    ...style,
                    display: 'block',
                    background: 'gray',
                    paddingRight: '1px',
                    paddingTop: '1.85px',
                    borderRadius: '50%',
                    boxShadow: '0px 2px 4px rgba(0, 0, 0, 0.2)',
                    transform: 'scale(1.2)',
                    marginRight: '20px',
                    marginTop: '-20px',
                }}
                onClick={onClick}
            />
        );
    }

    const sliderSettings = {
        slidesToShow: 3,
        slidesToScroll: 1,
        infinite: true,
        speed: 500,
        lazyLoad: true,
        nextArrow: <SampleNextArrow />,
        prevArrow: <SamplePrevArrow />,
    };

    return (
        <div className={cx('user__profile-posts-trending')}>
            <div className={cx('user__profile-posts-trending-wrapper')}>
                <div className={cx('slider-container')}>
                    <Slider {...sliderSettings}>
                        {/* {posts.slice(0, slice).map((post) => (
                        <div key={post.id}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/b">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/${post.category.slug}`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/${post.slug}`}>
                                    <p className={cx('title-post-sm', 'mt-[10px]')}>Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị</p>
                                </Link>
                                <Link to={`/user/${post.author.userName}`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>
                                        Bùi Xuân Hiếu
                                    </p>
                                </Link>
                            </div>
                        </div>
                    ))} */}
                        <div key={1}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/a">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={
                                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/quandiem`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/xung-ho`}>
                                    <p className={cx('title-post', 'mt-[10px]')}>
                                        Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị
                                    </p>
                                </Link>
                                <Link to={`/user/xunhiu`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>Bùi Xuân Hiếu</p>
                                </Link>
                            </div>
                        </div>
                        <div key={2}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/b">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={
                                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/quandiem`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/xung-ho`}>
                                    <p className={cx('title-post', 'mt-[10px]')}>
                                        Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị
                                    </p>
                                </Link>
                                <Link to={`/user/xunhiu`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>Bùi Xuân Hiếu</p>
                                </Link>
                            </div>
                        </div>
                        <div key={3}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/b">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={
                                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/quandiem`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/xung-ho`}>
                                    <p className={cx('title-post', 'mt-[10px]')}>
                                        Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị
                                    </p>
                                </Link>
                                <Link to={`/user/xunhiu`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>Bùi Xuân Hiếu</p>
                                </Link>
                            </div>
                        </div>
                        <div key={4}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/b">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={
                                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/quandiem`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/xung-ho`}>
                                    <p className={cx('title-post', 'mt-[10px]')}>
                                        Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị
                                    </p>
                                </Link>
                                <Link to={`/user/xunhiu`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>Bùi Xuân Hiếu</p>
                                </Link>
                            </div>
                        </div>
                        <div key={5}>
                            <div className={cx('pom__content-details')}>
                                <Link to="/post/b">
                                    <div className={cx('mt-[10px]')}>
                                        <img
                                            className={cx('border-img', 'trending-img')}
                                            src={
                                                'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                            }
                                            alt="img"
                                        />
                                    </div>
                                </Link>
                                <Link to={`/category/quandiem`}>
                                    <p className={cx('title-category', 'mt-[10px]')}>QUAN ĐIỂM - TRANH LUẬN</p>
                                </Link>
                                <Link to={`/post/xung-ho`}>
                                    <p className={cx('title-post', 'mt-[10px]')}>
                                        Xưng hô trong tiếng Việt - lắm nghịch lý và kỳ thị
                                    </p>
                                </Link>
                                <Link to={`/user/xunhiu`}>
                                    <p className={cx('post-username', 'mt-[10px]')}>Bùi Xuân Hiếu</p>
                                </Link>
                            </div>
                        </div>
                    </Slider>
                </div>
            </div>
        </div>
    );
}

export default TrendingPosts;
