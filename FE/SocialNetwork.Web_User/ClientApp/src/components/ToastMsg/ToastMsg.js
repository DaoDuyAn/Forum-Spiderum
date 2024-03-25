import { useEffect, useRef } from 'react';
import classNames from 'classnames/bind';

import styles from './ToastMsg.module.scss';

const cx = classNames.bind(styles);

function ToastMsg({ message }) {
    const toast = useRef(null);
    
    useEffect(() => {
        const timer = setTimeout(() => {
            toast.current.style.animation = 'hide_slide 1s ease forwards';
        }, 4000);

        return () => clearTimeout(timer);
    }, [message]);

    return (
        <div>
            <button ref={toast} className={cx('snackbar', 'alert-toast-message')}>
                {message}
            </button>
        </div>
    );
}

export default ToastMsg;
