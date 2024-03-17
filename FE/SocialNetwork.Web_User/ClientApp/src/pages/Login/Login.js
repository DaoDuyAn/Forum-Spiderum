import { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';

import classNames from 'classnames/bind';
import styles from './Login.module.scss';

const cx = classNames.bind(styles);

function Login() {
    const [data, setData] = useState({ userName: '', password: '' });
    const [errMessage, setErrMessage] = useState(null);

    const handleAuthGoogle = () => {};

    const onSubmit = () => {};

    return (
        <div className={cx('login')}>
            <div className={cx('login__container')}>
                <Link to="/" className={cx('login__logo')}>
                    <img src="https://auth.spiderum.com/assets-auth/images/spiderum-logo.png" alt="" />
                </Link>

                <p>{errMessage}</p>

                <form action="" method="POST" className={cx('login__form')}>
                    <input
                        type="text"
                        placeholder="Tên đăng nhập hoặc email"
                        name="userName"
                        className={cx('login__form-input')}
                        value={data.userName}
                        onChange={(e) => setData({ ...data, userName: e.target.value })}
                    />
                    <input
                        type="password"
                        placeholder="Mật khẩu"
                        name="password"
                        className={cx('login__form-input')}
                        value={data.password}
                        onChange={(e) => setData({ ...data, password: e.target.value })}
                    />
                    <button className={cx('login__form-button', 'bg-button')} type="submit" onClick={onSubmit}>
                        Đăng nhập
                    </button>
                </form>

                <p className={cx('login__text')}>Đăng nhập bằng</p>
                <div className={cx('login__google')}>
                    <div className={cx('login__google-link')}>
                        <button className={cx('login__form-button', 'gg')} onClick={handleAuthGoogle}>
                            <FontAwesomeIcon className={cx('login__icon')} icon={faGoogle} />
                            <span className={cx('login__text', 'gg')}>Google</span>
                        </button>
                    </div>
                </div>
                {/* <Link to="/">
                <p className={cx('login__text', 'link')}>Quên mật khẩu?</p>
            </Link> */}
                <span className={cx('login__text')}>Không có tài khoản?</span>
                <a href="/register">
                    <span className={cx('login__text', 'link')}> Đăng ký ngay</span>
                </a>
            </div>
        </div>
    );
}

export default Login;
