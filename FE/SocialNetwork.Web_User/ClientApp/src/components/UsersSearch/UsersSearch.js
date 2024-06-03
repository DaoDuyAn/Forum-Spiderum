import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';

import styles from './UsersSearch.module.scss';

const cx = classNames.bind(styles);

 function UsersSearch({ user }) {

    return (
        // <div className={cx('user-container')}>
            <Link to={`/user/${user.userName}`} className={cx('card-user')}>
                <img
                    src={
                        user.avatar
                            ? user.avatar
                            : 'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'
                    }
                    alt="ava_img"
                    className={cx('post-avt')}
                />

                <div className={cx('user-info')}>
                    <Link to={`/user/${user.userName}`} className={cx('post-username')}>
                        <span>{user.userName}</span>
                    </Link>
                </div>
            </Link>
        // </div>
   
            // <Link to={`/user/`} className={cx('card-user')}>
            //     <img
            //         src={'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'}
            //         alt="ava_img"
            //         className={cx('post-avt')}
            //     />

            //     <div className={cx('user-info')}>
            //         <Link to={`/user/`} className={cx('post-username')}>
            //             <span>daoduyan</span>
            //         </Link>
            //     </div>
            // </Link>
     
    );
}

export default UsersSearch;
