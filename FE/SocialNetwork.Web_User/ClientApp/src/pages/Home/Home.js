import classNames from 'classnames/bind';

import Filter from '~/components/Filter';
import Sidebar from '~/components/Sidebar';
import styles from './Home.module.scss';

const cx = classNames.bind(styles);

function Home() {
    return (
        <main>
            <div className={cx('grid')}>
                <div className={cx('row')}>
                    <div className={cx('w-full', 'sm:w-8/12')}>
                        <Filter />
                    </div>
                    <div className={cx('hidden', 'sm:block', 'sm:w-4/12')}>
                        <Sidebar />
                    </div>
                </div>
            </div>
        </main>
    );
}

export default Home;
