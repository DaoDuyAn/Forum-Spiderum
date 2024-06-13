import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHandHoldingHeart } from '@fortawesome/free-solid-svg-icons';

import DatePost from '../DatePost';
import styles from './Sidebar.module.scss';

const cx = classNames.bind(styles);

function Sidebar() {
    const userId = localStorage.getItem('userId') ?? null;

    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const userIdToUse = userId || '00000000-0000-0000-0000-000000000000'; 
                const response = await axios.get('https://localhost:44379/api/v1/GetPosts', {
                    params: {
                        sort: 'top',
                        page_idx: 1,
                        userId: userIdToUse,
                    },
                });
                setPosts(response.data.postResponse);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPosts();
    }, []);

    return (
        <div className={cx('adv')}>
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
