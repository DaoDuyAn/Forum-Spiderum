import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faSignOut, faGear, faPenToSquare, faUserPen } from '@fortawesome/free-solid-svg-icons';
import { faEnvelope, faBookmark, faBell } from '@fortawesome/free-regular-svg-icons';
import Tippy from '@tippyjs/react';
import { Link } from 'react-router-dom';
import Tabs, { tabsClasses } from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';

import config from '~/config';
import Button from '~/components/Button';
import styles from './Header.module.scss';
import 'tippy.js/dist/tippy.css';
import Menu from '~/components/Popper/Menu';
import Image from '~/components/Image';
import Search from '../Search';

import { styled } from '@mui/system';

const CustomTab = styled(Tab)({
    fontSize: '1.25rem',
});

const cx = classNames.bind(styles);

function Header() {
    const currentUser = true;
    const [value, setValue] = useState(parseInt(localStorage.getItem('activeTab')) ?? -1);
    const [categories, setCategories] = useState([]);

    
    // useEffect(() => {
    //     const storedValue = localStorage.getItem('activeTab');
    //     console.log(storedValue);
    //     if (storedValue !== null) {
    //         setValue(parseInt(storedValue));
    //     }
    // }, []);

    useEffect(() => {
        localStorage.setItem('activeTab', JSON.stringify(value));
    }, [value]);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    // Handle logic
    const handleMenuChange = (menuItem) => {
        // console.log(menuItem);
    };

    const userMenu = [
        {
            icon: <FontAwesomeIcon icon={faUser} />,
            title: 'Xem trang cá nhân',
            to: '/user/an',
        },
        {
            icon: <FontAwesomeIcon icon={faPenToSquare} />,
            title: 'Bài viết của tôi',
            to: '/user/duyan?tab=createdPosts',
        },
        {
            icon: <FontAwesomeIcon icon={faBookmark} />,
            title: 'Đã lưu',
            to: '/user/duyan?tab=savedPosts',
        },
        {
            icon: <FontAwesomeIcon icon={faGear} />,
            title: 'Tùy chỉnh tài khoản',
            to: '/user/settings',
        },
        {
            icon: <FontAwesomeIcon icon={faSignOut} />,
            title: 'Đăng xuất',
            to: '/login',
            separate: true,
        },
    ];

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get(`https://localhost:44379/api/v1/Category`);
                const data = response.data;

                setCategories(data);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchCategories();
    }, []);

    return (
        <header className={cx('wrapper', currentUser ? 'height1' : 'height2')}>
            <div className={cx('inner')}>
                <Link to={config.routes.home} className={cx('logo-link')} onClick={() => setValue(-1)}>
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

                            <Link to={config.routes.create}>
                                <Button className={'ml-3'} outline leftIcon={<FontAwesomeIcon icon={faUserPen} />}>
                                    Viết bài
                                </Button>
                            </Link>
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
                    <Tabs
                        value={value}
                        onChange={handleChange}
                        variant="scrollable"
                        scrollButtons
                        aria-label="visible arrows tabs example"
                        sx={{
                            [`& .${tabsClasses.scrollButtons}`]: {
                                '&.Mui-disabled': { opacity: 0.3 },
                                '& .MuiSvgIcon-root': { fontSize: '2rem' },
                            },
                        }}
                    >
                        {categories.map((category, index) => (
                              <CustomTab key={index} label={category.categoryName} component={Link} to={`/category/${category.slug}`} />
                        ))}
                        
                    </Tabs>
                </div>
            ) : (
                <></>
            )}
        </header>
    );
}

export default Header;
