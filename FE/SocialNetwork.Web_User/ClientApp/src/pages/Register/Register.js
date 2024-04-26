import { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import classNames from 'classnames/bind';

import config from '~/config';
import styles from './Register.module.scss';

const cx = classNames.bind(styles);

function Register() {
    let navigate = useNavigate();

    const [messages, setMessages] = useState(null);
    const [err, setErr] = useState(true);
    const [data, setData] = useState({
        userName: '',
        fullName: '',
        password: '',
        phone: '',
        roleName: 'User',
    });

    const onSubmit = async (e) => {
        e.preventDefault();
        try {
            console.log(data);
            const response = await axios.post('https://localhost:44379/api/v1/SignUp', data);

            if (response.data === 1) {
                setMessages('Đăng ký thành công.');
                setErr(false);

                navigate('/login');
            } else if (response.data === -1) {
                setMessages('Tên đăng nhập đã tồn tại.');
                setErr(true);
            } else if (response.data === -2) {
                setMessages('Số điện thoại đã được sử dụng.');
                setErr(true);
            } else {
                setMessages('Đã có lỗi xảy ra khi đăng ký.');
                setErr(true);
            }
        } catch (error) {
            setMessages('Đã có lỗi xảy ra khi đăng ký.');
            setErr(true);
            console.error('Error registering:', error);
        }
    };

    return (
        <>
            {messages ? (
                <div className={cx('alert-auth')}>
                    <div className={cx('alert-mess', err ? 'err' : '')}>
                        <div>{messages} </div>
                    </div>
                </div>
            ) : (
                <></>
            )}

            <div className={cx('register')}>
                <Link to="/" className={cx('login__logo')}>
                    <img src="https://auth.spiderum.com/assets-auth/images/spiderum-logo.png" alt="" />
                </Link>

                <form onSubmit={onSubmit} className={cx('login__form')}>
                    <input
                        type="text"
                        placeholder="Tên đăng nhập"
                        name="userName"
                        className={cx('login__form-input')}
                        value={data.userName}
                        required
                        onChange={(e) => setData({ ...data, userName: e.target.value })}
                    />

                    <input
                        type="text"
                        placeholder="Tên hiển thị"
                        name="fullName"
                        className={cx('login__form-input')}
                        value={data.fullName}
                        required
                        onChange={(e) => setData({ ...data, fullName: e.target.value })}
                    />

                    <input
                        type="password"
                        placeholder="Mật khẩu"
                        name="password"
                        className={cx('login__form-input')}
                        value={data.password}
                        required
                        onChange={(e) => setData({ ...data, password: e.target.value })}
                    />

                    <input
                        type="text"
                        placeholder="Số điện thoại"
                        name="phone"
                        className={cx('login__form-input')}
                        value={data.phone}
                        required
                        onChange={(e) => setData({ ...data, phone: e.target.value })}
                    />

                    <Link to={config.routes.auth_register}>
                        <p className={cx('login__text')}>Đăng ký bằng email</p>
                    </Link>
                    <button className={cx('login__form-button', 'bg-button')} name="btnSubmit" type="submit">
                        Đăng ký
                    </button>
                    <div className={cx('auth__back')}>
                        <p>
                            <span className={cx('auth__register-tilte')}>Đã có tài khoản? </span>
                            <Link to="/login" className={cx('auth__back-login')}>
                                Đăng nhập
                            </Link>
                        </p>
                    </div>
                </form>
            </div>
        </>
    );
}

export default Register;
