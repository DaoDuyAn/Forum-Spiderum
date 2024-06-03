import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { Link, useParams } from 'react-router-dom';
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
    const [filterActive, setFilterActive] = useState(parseInt(localStorage.getItem('filterActive')) || 0);
    const [posts, setPosts] = useState([]);
    const [sort, setSort] = useState(localStorage.getItem('sort') || 'hot');
    const [pageIdx, setPageIdx] = useState(parseInt(localStorage.getItem('page_idx')) || 1);
    const userId = localStorage.getItem('userId') ?? null;
    const [pageCount, setPageCount] = useState(1);

    useEffect(() => {
        window.scrollTo({
            top: 0,
            left: 0,
        });
    }, [sort, pageIdx]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const userIdToUse = userId || '00000000-0000-0000-0000-000000000000'; 
                const response = await axios.get('https://localhost:44379/api/v1/GetPosts', {
                    params: {
                        sort: sort,
                        page_idx: pageIdx,
                        userId: userIdToUse,
                    },
                });
                setPosts(response.data.postResponse);
                setPageCount(response.data.pageCount);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPosts();
    }, [sort, pageIdx]);

    const authenticatedFilters = [
        {
            displayName: 'DÀNH CHO BẠN',
            path: `/?sort=hot&page_idx=${pageIdx}`, 
            sort: 'hot'
        },
        {
            displayName: 'THEO TÁC GIẢ',
            path: `/?sort=follow&page_idx=${pageIdx}`,
            sort: 'follow'
        },
        {
            displayName: 'MỚI NHẤT',
            path: `/?sort=news&page_idx=${pageIdx}`,
            sort: 'news'
        },
        {
            displayName: 'SÔI NỔI',
            path: `/?sort=controversial&page_idx=${pageIdx}`, 
            sort: 'controversial'
        },
        {
            displayName: 'ĐÁNH GIÁ CAO NHẤT',
            path: `/?sort=top&page_idx=${pageIdx}`,
            sort: 'top'
        },
    ];
    

    const unauthenticatedFilters = [
        {
            displayName: 'DÀNH CHO BẠN',
            path: `/?sort=hot&page_idx=${pageIdx}`, 
            sort: 'hot'
        },
        {
            displayName: 'MỚI NHẤT',
            path: `/?sort=news&page_idx=${pageIdx}`,
            sort: 'news'
        },
        {
            displayName: 'SÔI NỔI',
            path: `/?sort=controversial&page_idx=${pageIdx}`, 
            sort: 'controversial'
        },
        {
            displayName: 'ĐÁNH GIÁ CAO NHẤT',
            path: `/?sort=top&page_idx=${pageIdx}`,
            sort: 'top'
        },
    ];

    const filterList = userId ? authenticatedFilters : unauthenticatedFilters;

    const handleSortChange = (newSort) => {
        localStorage.setItem('sort', newSort);
        setSort(newSort);
        setPageIdx(1); // Reset về trang đầu tiên khi thay đổi sort
    };

    const handleFilterActive = (index) => {
        localStorage.setItem('filterActive', index.toString());
        setFilterActive(index);
    };

    const handlePageIdxChange = (newPageIdx) => {
        localStorage.setItem('page_idx', newPageIdx.toString());
        setPageIdx(newPageIdx);
    };

    return (
        <section className={cx('filter')}>
            <div className={cx('filter__wrapper')}>
                <div className={cx('filter__bar')}>
                    <div className={cx('filter__sort')}>
                        {filterList.map((e, i) => (
                            <Link
                                key={i}
                                to={e.path}
                                className={cx('filter__sort-item', { active: filterActive === i })}
                                onClick={() => {
                                    handleFilterActive(i);
                                    handleSortChange(e.sort); 
                                }}
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
                                        {posts && posts.map((post) => <PostItem key={post._id} post={post} />)}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className={cx('flex', 'justify-center')}>
                    <StyledPagination
                        count={pageCount} // Số trang được cập nhật từ state
                        page={pageIdx} // Trang hiện tại
                        onChange={(event, page) => handlePageIdxChange(page)}
                        variant="outlined"
                        color="primary"
                        size="large"
                        boundaryCount={2}
                    />
                </div>
            </div>
        </section>
    );
}

export default Filter;
