import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { Link, useParams } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHandHoldingHeart, faCircleCheck } from '@fortawesome/free-solid-svg-icons';

import DatePost from '../DatePost';
import styles from './SidebarCate.module.scss';

const cx = classNames.bind(styles);

function Sidebar({category}) {
    const { slug } = useParams();

    const [isFollow, setIsFollow] = useState(false);
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const response = await axios.get('https://localhost:44379/api/v1/GetPostsByCategory', {
                    params: {
                        sort: 'top',
                        page_idx: 1,
                        slug: slug,
                    },
                });

                setPosts(response.data.postResponse);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPosts();
    }, [slug]);

    return (
        <div className={cx('adv')}>
            <div className={cx('adv__policy', 'box-shadow')}>
                <div className={cx('adv__policy-title')}>{category.categoryName}</div>
                <div className={cx('adv__policy-content')}>
                    <p className={cx('widget-title')}>Nội dung cho phép</p>
                    <p className={cx('widget-content')}>
                       {category.contentAllowed}
                    </p>
                    <p className={cx('widget-title')}>Quy định</p>
                    <ul className={cx('sidebar__policy-menu')}>
                        {/* {policy && policy.map((e, i) => (
                          <li key={i}>{e.content}</li>
                        ))} */}
                        <li key={1}>
                            Những nội dung không thuộc phạm trù của danh mục sẽ bị nhắc nhở và xoá (nếu không thay đổi
                            thích hợp)
                        </li>
                        <li key={2}>Nghiêm cấm spam, quảng cáo</li>
                        <li key={3}>
                            Nghiêm cấm post nội dung 18+ hay những quan điểm cực đoan liên quan tới chính trị - tôn giáo
                        </li>
                        <li key={4}>Nghiêm cấm phát ngôn thiếu văn hoá và đả kích cá nhân.</li>
                    </ul>
                </div>
                <div className={cx('adv__policy-link')}>
                    {isFollow ? (
                        <button className={cx('adv__policy-button', 'active')}>
                            <FontAwesomeIcon icon={faCircleCheck} className={cx('adv__policy-icon')} />
                            Đang theo dõi
                        </button>
                    ) : (
                        <button className={cx('adv__policy-button')}>Theo dõi</button>
                    )}
                </div>
            </div>

            <div className={cx('adv__donate', 'box-shadow')}>
                <p className={cx('adv__donate-content')}>
                    Bạn yêu thích cộng đồng Spiderum và muốn nó trở nên lớn mạnh hơn?
                </p>
                <Link to="/" className={cx('adv__donate-link')}>
                    <button className={cx('adv__donate-button')}>
                        <FontAwesomeIcon className={cx('adv__donate-icon')} icon={faHandHoldingHeart} />
                        Ủng hộ
                    </button>
                </Link>
            </div>

            <div className={cx('adv__widget', 'box-shadow')}>
                <p className={cx('adv__widget-title')}>CÓ THỂ BẠN QUAN TÂM</p>
                <div className={cx('adv__widget-content')}>
                    {posts.slice(0, 5).map((post) => (
                        <div className={cx('adv__widget-content-details')}>
                            <div className={cx('adv__widget-avt')}>
                                <Link to={`/user/an{post.author.userName}`}>
                                    <img
                                        src={
                                            post.postInfo.thumbnailImagePath !== ''
                                                ? post.postInfo.thumbnailImagePath
                                                : 'https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-thumbnails/defaultthumbnail.png'
                                        }
                                        alt="Ảnh của bài viết"
                                    />
                                </Link>
                            </div>
                            <div className={cx('adv__widget-user')}>
                                <Link to={`/post/${post.postInfo.slug}`}>
                                    <p className={cx('post-title')}>{post.postInfo.title}</p>
                                </Link>
                                <Link to={`/user/${post.userInfo.userName}`}>
                                    <span className={cx('username')}>
                                        {post.userInfo.fullName}
                                    </span>
                                </Link>
                                <DatePost date={post.postInfo.creationDate}></DatePost>
                            </div>
                        </div>
                    ))}       
                </div>
            </div>

            <div className={cx('adv__about')}>
                <ul className={cx('adv__about-list')}>
                    <li className={cx('adv__about-item')}>
                        <Link to="/" className={cx('adv__about-link')}>
                            <span className={cx('adv__about-text')}>Về Spiderum</span>
                        </Link>
                    </li>
                    <li className={cx('adv__about-item')}>
                        <Link to="/" className={cx('adv__about-link')}>
                            <span className={cx('adv__about-text')}>Điều khoản sử dụng</span>
                        </Link>
                    </li>
                    <li className={cx('adv__about-item')}>
                        <Link to="/" className={cx('adv__about-link')}>
                            <span className={cx('adv__about-text')}>Fangpage</span>
                        </Link>
                    </li>
                </ul>
                <span className={cx('adv__about-text')}>© 2024 Đào Duy An</span>
            </div>
        </div>
    );
}

export default Sidebar;
