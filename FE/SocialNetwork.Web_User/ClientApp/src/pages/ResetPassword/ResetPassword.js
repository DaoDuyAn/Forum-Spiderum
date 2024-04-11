import { useState, useCallback } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import classNames from 'classnames/bind';

import config from '~/config';

import styles from '../Login/Login.module.scss';

const cx = classNames.bind(styles);

function ResetPassword() {
    const [data, setData] = useState({ password: '', confirm_password: '' });
    const [messages, setMessages] = useState(null);
    const [err, setErr] = useState(true);

    const navigate = useNavigate();

    const hanldeCheckPassword = () => {
        if (data.password !== data.confirm_password) {
            setMessages('Mật khẩu mới và mật khẩu xác nhận không giống nhau');
            return;
        }

        if (data.password.length < 6 || data.password.length > 100) {
            setMessages('Mật khẩu nên chứa từ 6 đến 100 ký tự');
            return;
        }

        navigate(config.routes.home);
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (data.password.length !== 0 && data.confirm_password.length !== 0) {
            hanldeCheckPassword();
        }
    };

    return (
        <>
            {messages ? (
                <div className={cx('alert-auth')}>
                    <div className={cx('alert-mess', err ? 'err' : '')}>
                        <div>{messages}</div>
                    </div>
                </div>
            ) : (
                <></>
            )}
            <div className={cx('login')}>
                <div className={cx('login__container')}>
                    <Link to="/" className={cx('login__logo')}>
                        <img src="https://auth.spiderum.com/assets-auth/images/spiderum-logo.png" alt="" />
                    </Link>

                    <form onSubmit={handleSubmit} className={cx('login__form')}>
                        <p className={cx('reset-title')}>
                            Nhập lại mật khẩu mới cho tài khoản: <b>daoduyan</b>
                        </p>
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
                            type="password"
                            placeholder="Nhập lại mật khẩu"
                            name="password"
                            className={cx('login__form-input')}
                            value={data.confirm_password}
                            required
                            onChange={(e) => setData({ ...data, confirm_password: e.target.value })}
                        />
                 
                        <button className={cx('login__form-button', 'bg-button')} type="submit">
                            Xác nhận đặt lại mật khẩu
                        </button>
                    </form>
                </div>
            </div>
        </>
    );
}

export default ResetPassword;
