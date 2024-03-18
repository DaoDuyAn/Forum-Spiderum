import { useState, useCallback } from 'react';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';

import styles from './AuthRegister.module.scss';

const cx = classNames.bind(styles);

function AuthRegister() {
    const [email, setEMail] = useState({});
    const [visible, setVisible] = useState(true);
    const [messages, setMessages] = useState(null);
    const [err, setErr] = useState(true);
    const [otp, setOtp] = useState(0);

    const handleSubmit = useCallback((e) => {
        e.preventDefault();
    }, []);

    const handleAuthMail = useCallback((e) => {
        e.preventDefault();
    }, []);

    const handleSubmitOTP = useCallback((e) => {
        e.preventDefault();
    }, []);

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
            
            <div className={cx('auth')}>
                <div className={cx('auth__container')}>
                    <div className={cx('auth__content')}>
                        <div className={cx('auth__logo')}>
                            <Link to="/">
                                <img
                                    src="https://auth.spiderum.com/assets-auth/images/spiderum-logo.png"
                                    alt="spiderum_img"
                                />
                            </Link>
                        </div>

                        <div className={cx('auth__register')}>
                            <p className={cx('auth__register-tilte')}>Đăng ký bằng email</p>
                            <form className={cx('auth__register-email')} onSubmit={handleSubmit}>
                                <div className={cx('auth__register-email-sending')}>
                                    <input
                                        className={cx('auth__register-email-input')}
                                        value={email.mail}
                                        onChange={(e) => setEMail({ ...email, email: e.target.value })}
                                        type="text"
                                        placeholder="email@example.com"
                                    />
                                    <div className={cx('auth__register-email-send-otp')}>
                                        <p>Thư xác nhận sẽ được gửi vào hòm thư của bạn</p>
                                        <button
                                            className={cx('btn__form')}
                                            type="submit"
                                            value="Gửi"
                                            onClick={handleAuthMail}
                                        >
                                            Gửi
                                        </button>
                                    </div>
                                </div>
                            </form>

                            {visible ? (
                                <form action="" onSubmit={handleSubmit}>
                                    <div className={cx('otp')}>
                                        <input
                                            className={cx('auth__register-email-input')}
                                            value={otp.otp}
                                            onChange={(e) => setOtp({ ...otp, otp: e.target.value })}
                                            type="text"
                                            placeholder="Nhập mã OTP tại đây..."
                                        />
                                        <button className={cx('btn__form')} type="submit" onClick={handleSubmitOTP}>
                                            Xác nhận
                                        </button>
                                    </div>
                                </form>
                            ) : (
                                <></>
                            )}
                        </div>
                        <div className={cx('auth__back')}>
                            <p>
                                <span className={cx('auth__register-tilte')}>Đã có tài khoản? </span>
                                <Link to="/login" className={cx('auth__back-login')}>
                                    Đăng nhập
                                </Link>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default AuthRegister;
