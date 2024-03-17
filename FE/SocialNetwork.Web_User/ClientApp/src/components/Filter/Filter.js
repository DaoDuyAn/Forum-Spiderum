import { useState } from 'react';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import Pagination from '@mui/material/Pagination';

import PostItem from '../PostItem';
import styles from './Filter.module.scss';
import { styled } from '@mui/system';

const StyledPagination = styled(Pagination)({
    '& .MuiPaginationItem-root': {
        fontSize: '1.5rem', 
    },
});
const cx = classNames.bind(styles);

function Filter() {
    const [filterActive, setFilterActive] = useState(0);
    // const [page, setPage] = useState(12);
    // const [currentButton, setCurrentButton] = useState(1);

    // const handleSetPage = (pageNumber) => {
    //     setCurrentButton(pageNumber + 1);
    // };

    const fitterList = [
        {
            displayName: 'DÀNH CHO BẠN',
            path: '/',
        },
        {
            displayName: 'THEO TÁC GIẢ',
            path: '/',
        },
        {
            displayName: 'MỚI NHẤT',
            path: '/?sort=news',
        },
        {
            displayName: 'SÔI NỔI',
            path: '/?sort=controversial',
        },
        {
            displayName: 'ĐÁNH GIÁ CAO NHẤT',
            path: '/?sort=top',
        },
    ];

    const handleFilterActive = (index) => {
        setFilterActive(index);
    };

    return (
        <section className={cx('filter')}>
            <div className={cx('filter__wrapper')}>
                <div className={cx('filter__bar')}>
                    <div className={cx('filter__sort')}>
                        {fitterList.map((e, i) => (
                            <Link
                                key={i}
                                to={e.path}
                                className={cx('filter__sort-item', { active: filterActive === i })}
                                onClick={() => handleFilterActive(i)}
                            >
                                <span className={cx('filter__sort-text', { active: filterActive === i })}>
                                    {e.displayName}
                                </span>
                            </Link>
                        ))}
                    </div>
                </div>
                <div className={cx('filter__page')}>
                    <div></div>
                </div>

                <div className={cx('grid')}>
                    <div className={cx('row')}>
                        <div className={cx('col-span-12')}>
                            <div className={cx('filter__content')}>
                                <div className={cx('filter__content-details')}>
                                    <div className={cx('grid')}>
                                        <PostItem key={1} />
                                        <PostItem key={1} />
                                        <PostItem key={1} />
                                        <PostItem key={1} />
                                        <PostItem key={1} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className={cx('flex', 'justify-center')}>
                    <StyledPagination count={10} variant="outlined" color="primary" size="large" boundaryCount={2}/>
                </div>
            </div>
        </section>
    );
}

export default Filter;
