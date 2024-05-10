import { useState, useEffect } from 'react';
import axios from 'axios'; 
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
    const [posts, setPosts] = useState([]);
    const userId = localStorage.getItem('userId') ?? null;
    // const [page, setPage] = useState(12);
    // const [currentButton, setCurrentButton] = useState(1);

    // const handleSetPage = (pageNumber) => {
    //     setCurrentButton(pageNumber + 1);
    // };

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const response = await axios.get('https://localhost:44379/api/v1/GetAllPosts');
                setPosts(response.data); 
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPosts(); 
    }, []); 

    const fitterList = [
        {
            displayName: 'DÀNH CHO BẠN',
            path: '/?sort=hot',
        },
        {
            displayName: 'THEO TÁC GIẢ',
            path: '/?sort=follow',
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

                <div className={cx('grid')}>
                    <div className={cx('row')}>
                        <div className={cx('col-span-12')}>
                            <div className={cx('filter__content')}>
                                <div className={cx('filter__content-details')}>
                                    <div className={cx('grid')}>
                                        {posts && posts.map((post) => (
                                            <PostItem key={post._id} post={post} />
                                        ))}    
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
