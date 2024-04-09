import { useState } from 'react';
import classNames from 'classnames/bind';
import { Link } from 'react-router-dom';
import Pagination from '@mui/material/Pagination';
import { styled } from '@mui/system';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFire, faStar, faComments, faFlag } from '@fortawesome/free-solid-svg-icons';

import PostItem from '../PostItem';
import styles from './FilterCate.module.scss';

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
            displayName: 'THỊNH HÀNH',
            path: '/category/a/?sort=hot',
            icon: <FontAwesomeIcon  className={cx('filter__sort-icon')} icon={faFire} />,
        },
        {
            displayName: 'MỚI',
            path: '/category/a/?sort=new',
            icon: <FontAwesomeIcon  className={cx('filter__sort-icon')} icon={faStar} />,
        },
        {
            displayName: 'SÔI NỔI',
            path: '/category/a/?sort=controversial',
            icon: <FontAwesomeIcon  className={cx('filter__sort-icon')} icon={faComments} />,
        },
        {
            displayName: 'TOP',
            path: '/category/a/?sort=top',
            icon: <FontAwesomeIcon  className={cx('filter__sort-icon')} icon={faFlag} />,
        },
    ];

    const handleFilterActive = (index) => {
        setFilterActive(index);
    };

    return (
        <section className={cx('filter')}>
            <div className={cx('filter__wrapper')}>
                <div className={cx('filter__bar')}>
                    <div className={cx('title')}>
                        <span>DÀNH CHO BẠN</span>
                    </div>
                    <div className={cx('filter__sort')}>
                        {fitterList.map((e, i) => (
                            <Link
                                key={i}
                                to={e.path}
                                className={cx('filter__sort-item', { active: filterActive === i })}
                                onClick={() => handleFilterActive(i)}
                            >
                                {e.icon}
                                <span className={cx('filter__sort-text', 'ml-[6px]', { active: filterActive === i })}>
                                    {e.displayName}
                                </span>
                            </Link>
                        ))}
                    </div>
                </div>

                <div className={cx('grid')}>
                    <div className={cx('row')}>
                        <div className={cx('col-span-12')}>
                            <div className={cx('filter__content')}>
                                <div className={cx('filter__content-details')}>
                                    <div className={cx('grid')}>
                                        <PostItem key={1} />
                                        <PostItem key={2} />
                                        <PostItem key={3} />
                                        <PostItem key={4} />
                                        <PostItem key={5} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className={cx('flex', 'justify-center')}>
                    <StyledPagination count={10} variant="outlined" color="primary" size="large" boundaryCount={2} />
                </div>
            </div>
        </section>
    );
}

export default Filter;
