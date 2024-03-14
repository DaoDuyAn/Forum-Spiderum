import { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronUp } from '@fortawesome/free-solid-svg-icons';

import classNames from 'classnames/bind';

import styles from './Gototop.module.scss';

const cx = classNames.bind(styles);

function Footer() {
    const [visible, setVisible] = useState(false);

    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    };

    useEffect(() => {
        const handleShow = () => {
            let pageY = window.pageYOffset;
            if (pageY > 300) {
                setVisible(true);
            } else {
                setVisible(false);
            }
        };
        window.addEventListener('scroll', handleShow);
        return () => window.removeEventListener('scroll', handleShow);
    }, []);

    return (
        <div className={cx('top', visible ? '' : 'hide-icon')} onClick={scrollToTop}>
            <FontAwesomeIcon className={cx('top__icon')} icon={faChevronUp} />
        </div>
    );
}

export default Footer;
