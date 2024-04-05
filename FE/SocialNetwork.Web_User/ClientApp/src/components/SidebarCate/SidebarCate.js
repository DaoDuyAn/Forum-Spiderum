import { useState } from 'react';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHandHoldingHeart, faCircleCheck } from '@fortawesome/free-solid-svg-icons';

import DatePost from '../DatePost';
import styles from './SidebarCate.module.scss';

const cx = classNames.bind(styles);

function Sidebar({category}) {
    const d = new Date();
    const [isFollow, setIsFollow] = useState(false);

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
                    {/* {posts.data.slice(0, 5).map((post) => (
                        <div className={cx('adv__widget-content-details')}>
                            <div className={cx('adv__widget-avt')}>
                                <Link to={`/user/an{post.author.userName}`}>
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
                                <Link to={`/post/baiviet${post.slug} `}>
                                    <p className={cx('post-title')}>{post.title}</p>
                                </Link>
                                <Link to={`/user/an${post.author.userName} `}>
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
                            <Link to={`/post/baiviet`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/an`}>
                                <span className={cx('username')}>Duy An</span>
                            </Link>
                            <DatePost date={d}></DatePost>
                        </div>
                    </div>
                    <div className={cx('adv__widget-content-details')}>
                        <div className={cx('adv__widget-avt')}>
                            <Link to={`/user/ann`}>
                                <img
                                    src={
                                        'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                    }
                                    alt=""
                                />
                            </Link>
                        </div>
                        <div className={cx('adv__widget-user')}>
                            <Link to={`/post/baiviet`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/an`}>
                                <span className={cx('username')}>Duy An</span>
                            </Link>
                            <DatePost date={d}></DatePost>
                        </div>
                    </div>
                    <div className={cx('adv__widget-content-details')}>
                        <div className={cx('adv__widget-avt')}>
                            <Link to={`/user/ann`}>
                                <img
                                    src={
                                        'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                                    }
                                    alt=""
                                />
                            </Link>
                        </div>
                        <div className={cx('adv__widget-user')}>
                            <Link to={`/post/baiviet`}>
                                <div className={cx('post-title')}>
                                    Sự nghiệp Sơn Tùng trong 10 bài hát Sự nghiệp Sơn Tùng trong 10 bài hát
                                </div>
                            </Link>
                            <Link to={`/user/an`}>
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
