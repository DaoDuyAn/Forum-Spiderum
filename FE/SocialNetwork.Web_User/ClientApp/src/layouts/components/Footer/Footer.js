import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';

import styles from './Footer.module.scss';

const cx = classNames.bind(styles);

function Footer() {
    return (
        <footer className={cx('footer')}>
            <div className={cx('footer__container')}>
                <div className={cx('footer__about')}>
                    <div className={cx('footer__about-logo')}>
                        <img src="https://spiderum.com/assets/icons/wideLogo.png" alt="spiderum" width="90"/>
                    </div>
                    <div className={cx('footer__about-menu')}>
                        <ul className={cx('footer__about-list')}>
                            <li className={cx('footer__about-item')}>
                                <Link to="/">
                                    <span className={cx('footer__about-text')}>VỀ SPIDERUM</span>
                                </Link>
                            </li>
                            <li className={cx('footer__about-item')}>
                                <Link to="/">
                                    <span className={cx('footer__about-text')}>CHUYÊN MỤC</span>
                                </Link>
                            </li>
                            <li className={cx('footer__about-item')}>
                                <Link to="/">
                                    <span className={cx('footer__about-text')}>ĐIỀU KIỆN SỬ DỤNG</span>
                                </Link>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </footer>
    );
}

export default Footer;
