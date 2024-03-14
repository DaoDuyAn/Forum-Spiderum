import { useRef } from 'react';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faUser,
    faSignOut,
    faGear,
    faPenToSquare,
    faUserPen,
    faChevronLeft,
    faChevronRight,
} from '@fortawesome/free-solid-svg-icons';
import { faEnvelope, faBookmark, faBell } from '@fortawesome/free-regular-svg-icons';
import Tippy from '@tippyjs/react';
import 'tippy.js/dist/tippy.css';
import { Link } from 'react-router-dom';

import config from '~/config';
import Button from '~/components/Button';
import styles from './Header.module.scss';
import Menu from '~/components/Popper/Menu';
import Image from '~/components/Image';
import Search from '../Search';

const cx = classNames.bind(styles);

function Header() {
    const currentUser = true;


    // Handle logic
    const handleMenuChange = (menuItem) => {
        // console.log(menuItem);
    };


    const listRef = useRef(null);

    const scrollLeft = () => {
        if (listRef.current) {
            listRef.current.scrollLeft -= 100;
        }
    };

    const scrollRight = () => {
        if (listRef.current) {
            listRef.current.scrollLeft += 100;
        }
    };

    const userMenu = [
        {
            icon: <FontAwesomeIcon icon={faUser} />,
            title: 'Xem trang cá nhân',
            to: '/@hoaa',
        },
        {
            icon: <FontAwesomeIcon icon={faPenToSquare} />,
            title: 'Bài viết của tôi',
            to: '/settings',
        },
        {
            icon: <FontAwesomeIcon icon={faBookmark} />,
            title: 'Đã lưu',
            to: '/feedack',
        },
        {
            icon: <FontAwesomeIcon icon={faGear} />,
            title: 'Tùy chỉnh tài khoản',
            to: '/feedack',
        },
        {
            icon: <FontAwesomeIcon icon={faSignOut} />,
            title: 'Đăng xuất',
            to: '/logout',
            separate: true,
        },
    ];

    return (
        <header className={cx('wrapper', currentUser ? 'height1' : 'height2')}>
            <div className={cx('inner')}>
                <Link to={config.routes.home} className={cx('logo-link')}>
                    <img src="https://spiderum.com/assets/icons/wideLogo.png" alt="spiderum" width="140" />
                </Link>

                <Search />

                <div className={cx('actions')}>
                    {currentUser ? (
                        <>
                            <Tippy delay={[0, 50]} content="Tin nhắn" placement="bottom">
                                <button className={cx('action-btn')}>
                                    <FontAwesomeIcon icon={faEnvelope} />
                                </button>
                            </Tippy>

                            <Tippy delay={[0, 50]} content="Thông báo" placement="bottom">
                                <button className={cx('action-btn')}>
                                    <FontAwesomeIcon icon={faBell} />
                                    <span className={cx('badge')}>12</span>
                                </button>
                            </Tippy>
                            
                            <Button className={'ml-3'} outline leftIcon={<FontAwesomeIcon icon={faUserPen} />}>
                                Viết bài
                            </Button>
                        </>
                    ) : (
                        <>
                            <Button text>Liên hệ</Button>
                            <Button text>Đăng ký</Button>
                            <Button primary>Đăng nhập</Button>
                        </>
                    )}

                    <Menu items={userMenu} onChange={handleMenuChange}>
                        {currentUser ? (
                            <Image
                                className={cx('user-avatar')}
                                src="https://www.gravatar.com/avatar/8f9a66cc24f92fb53bc4f112cf5a3fe2?d=wavatar&f=y"
                                alt="Avatar"
                            />
                        ) : (
                            <></>
                        )}
                    </Menu>
                </div>
            </div>
            {currentUser ? (
                 <div className={cx('header__menu')}>
                 <div className={cx('header__menu-category')}>
                     <div className={cx('header__menu-category-wrapper')}>
                         <div className={cx('header__menu-category-icon', 'left')} onClick={scrollLeft}>
                             <FontAwesomeIcon icon={faChevronLeft} />
                         </div>
                         <div className={cx('header__menu-navbar')} ref={listRef}>
                             {/* <ul className="header__menu-list" ref={listRef}>
                       {categorise.data.map((e, i) => (
                         <li key={i._id} className="header__menu-item">
                           <Link
                             to={`/category/${e.slug}`}
                             className="header__menu-link"
                           >
                             {e.name}
                           </Link>
                         </li>
                       ))}
                     </ul> */}
                             <ul className={cx('header__menu-list')}>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         QUAN ĐIỂM - TRANH LUẬN
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         KHOA HỌC - CÔNG NGHỆ
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         TÀI CHÍNH
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         THỂ THAO
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         QUAN ĐIỂM - TRANH LUẬN
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         KHOA HỌC - CÔNG NGHỆ
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         TÀI CHÍNH
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         THỂ THAO
                                     </Link>
                                 </li>
                                 <li className={cx('header__menu-item')}>
                                     <Link to={`/category`} className={cx('header__menu-link')}>
                                         TÀI CHÍNH
                                     </Link>
                                 </li>
                             </ul>
                         </div>
                         <div className={cx('header__menu-category-icon', 'right')} onClick={scrollRight }>
                             <FontAwesomeIcon icon={faChevronRight} />
                         </div>
                     </div>
                 </div>
             </div>
            ) : (
                <></>
            ) }
           
        </header>
    );
}

export default Header;
