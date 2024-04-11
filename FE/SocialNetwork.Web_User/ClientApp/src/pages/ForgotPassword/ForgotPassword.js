    import { useState, useCallback } from 'react';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';

import styles from '../AuthRegister/AuthRegister.module.scss';

const cx = classNames.bind(styles);

function ForgotPassword() {
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
                <div className={cx('alert-container')}>
                    <div className={cx('alert-mess', err ? 'err' : '')}>
                        <div>{messages}</div>
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
                            <p className={cx('auth__register-tilte')}>Vui lòng nhập địa chỉ email của bạn để được cấp lại mật khẩu</p>
                            <form className={cx('auth__register-email')} onSubmit={handleSubmit}>
                                <div className={cx('auth__register-email-sending')}>
                                    <input
                                        className={cx('auth__register-email-input')}
                                        value={email.mail}
                                        onChange={(e) => setEMail({ ...email, email: e.target.value })}
                                        type="text"
                                        required
                                        placeholder="email@example.com"
                                    />
                                    <div className={cx('auth__register-email-send-otp')}>
                                        <p>Thư xác nhận sẽ được gửi vào hòm thư của bạn</p>
                                        <button
                                            className={cx('btn__form', 'ml-auto')}
                                            type="submit"
                                            value="Gửi"
                                            onClick={handleAuthMail}
                                        >
                                            Gửi
                                        </button>
                                    </div>
                                </div>
                            </form>

                           
                        </div>
             
                    </div>
                </div>
            </div>
        </>
    );
}

export default ForgotPassword;
