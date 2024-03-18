import { useState } from 'react';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';

import styles from './Register.module.scss';

const cx = classNames.bind(styles);

function Register() {
    const [messages, setMessages] = useState(null);
    const [err, setErr] = useState(true);
    const [data, setData] = useState({
        userName: '',
        displayName: '',
        password: '',
        phone: '',
    });

    const onSubmit = () => {};

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

                <form action="" className={cx('login__form')} method="POST">
                    <input
                        type="text"
                        placeholder="Tên đăng nhập"
                        name="userName"
                        className={cx('login__form-input')}
                        value={data.userName}
                        onChange={(e) => setData({ ...data, userName: e.target.value })}
                    />

                    <input
                        type="text"
                        placeholder="Tên hiển thị"
                        name="displayName"
                        className={cx('login__form-input')}
                        value={data.displayName}
                        onChange={(e) => setData({ ...data, displayName: e.target.value })}
                    />

                    <input
                        type="password"
                        placeholder="Mật khẩu"
                        name="password"
                        className={cx('login__form-input')}
                        value={data.password}
                        onChange={(e) => setData({ ...data, password: e.target.value })}
                    />

                    <input
                        type="text"
                        placeholder="Số điện thoại"
                        name="phone"
                        className={cx('login__form-input')}
                        value={data.phone}
                        onChange={(e) => setData({ ...data, phone: e.target.value })}
                    />

                    <button
                        className={cx('login__form-button', 'bg-button')}
                        name="btnSubmit"
                        type="submit"
                        onClick={onSubmit}
                    >
                        Đăng ký
                    </button>
                </form>
            </div>
        </>
    );
}

export default Register;
