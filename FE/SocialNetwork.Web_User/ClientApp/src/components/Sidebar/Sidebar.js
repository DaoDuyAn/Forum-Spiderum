import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHandHoldingHeart } from '@fortawesome/free-solid-svg-icons';

import DatePost from '../DatePost';
import styles from './Sidebar.module.scss';

const cx = classNames.bind(styles);

function Sidebar() {
    const d = new Date();

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
                    {/* {posts.data.slice(0, 5).map((post) => (
                        <div className={cx('adv__widget-content-details')}>
                            <div className={cx('adv__widget-avt')}>
                                <Link to={`/user/${post.author.userName}`}>
                                    <img
                                        src={
                                            post.author.avatar
                                                ? post.author.avatar
                                                : 'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                        }
                                        alt=""
                                    />
                                </Link>
                            </div>
                            <div className={cx('adv__widget-user')}>
                                <Link to={`/post/${post.slug} `}>
                                    <p className={cx('post-title')}>{post.title}</p>
                                </Link>
                                <Link to={`/user/${post.author.userName} `}>
                                    <span className={cx('username')}>
                                        {post.author.displayName ? post.author.displayName : post.author.userName}{' '}
                                    </span>
                                </Link>
                                <DatePost date={post.createdAt}></DatePost>
                            </div>
                        </div>
                    ))} */}
                    <div className={cx('adv__widget-content-details')}>
                        <div className={cx('adv__widget-avt')}>
                            <Link to={`/user/an`}>
                                <img
                                    src={
                                        'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                    }
                                    alt=""
                                />
                            </Link>
                        </div>
                        <div className={cx('adv__widget-user')}>
                            <Link to={`/post/`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/ `}>
                                <span className={cx('username')}>Duy An</span>
                            </Link>
                            <DatePost date={d}></DatePost>
                        </div>
                    </div>
                    <div className={cx('adv__widget-content-details')}>
                        <div className={cx('adv__widget-avt')}>
                            <Link to={`/user/an`}>
                                <img
                                    src={
                                        'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                    }
                                    alt=""
                                />
                            </Link>
                        </div>
                        <div className={cx('adv__widget-user')}>
                            <Link to={`/post/`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/ `}>
                                <span className={cx('username')}>Duy An</span>
                            </Link>
                            <DatePost date={d}></DatePost>
                        </div>
                    </div>
                    <div className={cx('adv__widget-content-details')}>
                        <div className={cx('adv__widget-avt')}>
                            <Link to={`/user/an`}>
                                <img
                                    src={
                                        'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                    }
                                    alt=""
                                />
                            </Link>
                        </div>
                        <div className={cx('adv__widget-user')}>
                            <Link to={`/post/`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/ `}>
                                <span className={cx('username')}>Duy An</span>
                            </Link>
                            <DatePost date={d}></DatePost>
                        </div>
                    </div>
                </div>
            </div>

            <div className={cx('adv__about')}>
                <ul className={cx('adv__about-list')}>
                    <li className={cx('adv__about-item')}>
                        <Link to="/" className={cx('adv__about-link')}>
                            <span className={cx('adv__about-text')}>Về Speirum</span>
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
