import { useState, useEffect } from 'react';
import axios from 'axios';
import classNames from 'classnames/bind';
import { Link, useParams } from 'react-router-dom';
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
    const [filterActive, setFilterActive] = useState(parseInt(localStorage.getItem('filterCateActive')) || 0);
    const [sort, setSort] = useState(localStorage.getItem('sort') || 'hot');
    const [pageIdx, setPageIdx] = useState(parseInt(localStorage.getItem('page_idx')) || 1);
    const [posts, setPosts] = useState([]);
    const [pageCount, setPageCount] = useState(1);

    const { slug } = useParams();

    useEffect(() => {
        localStorage.setItem('sort', 'hot');
        localStorage.setItem('page_idx', '1');
        localStorage.setItem('filterCateActive', '0');
        setSort('hot');
        setPageIdx(1);
        setFilterActive(0);
    }, [slug]);

    useEffect(() => {
        window.scrollTo({
            top: 340,
            left: 0,
        });
    }, [sort, pageIdx]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const response = await axios.get('https://localhost:44379/api/v1/GetPostsByCategory', {
                    params: {
                        sort: sort,
                        page_idx: pageIdx,
                        slug: slug,
                    },
                });

                setPosts(response.data.postResponse);
                setPageCount(response.data.pageCount);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };

        fetchPosts();
    }, [sort, pageIdx, slug]);

    const fitterList = [
        {
            displayName: 'THỊNH HÀNH',
            path: `/category/${slug}/?sort=hot&page_idx=${pageIdx}`,
            icon: <FontAwesomeIcon className={cx('filter__sort-icon')} icon={faFire} />,
            sort: 'hot',
        },
        {
            displayName: 'MỚI',
            path: `/category/${slug}/?sort=news&page_idx=${pageIdx}`,
            icon: <FontAwesomeIcon className={cx('filter__sort-icon')} icon={faStar} />,
            sort: 'news',
        },
        {
            displayName: 'SÔI NỔI',
            path: `/category/${slug}/?sort=controversial&page_idx=${pageIdx}`,
            icon: <FontAwesomeIcon className={cx('filter__sort-icon')} icon={faComments} />,
            sort: 'controversial',
        },
        {
            displayName: 'TOP',
            path: `/category/${slug}/?sort=top&page_idx=${pageIdx}`,
            icon: <FontAwesomeIcon className={cx('filter__sort-icon')} icon={faFlag} />,
            sort: 'top',
        },
    ];

    const handleSortChange = (newSort) => {
        localStorage.setItem('sort', newSort);
        setSort(newSort);
        setPageIdx(1); // Reset về trang đầu tiên khi thay đổi sort
    };

    const handleFilterActive = (index) => {
        localStorage.setItem('filterCateActive', index.toString());
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
                    <div className={cx('title')}>
                        <span>DÀNH CHO BẠN</span>
                    </div>
                    <div className={cx('filter__sort')}>
                        {fitterList.map((e, i) => (
                            <Link
                                key={i}
                                to={e.path}
                                className={cx('filter__sort-item', { active: filterActive === i })}
                                onClick={() => {
                                    handleFilterActive(i);
                                    handleSortChange(e.sort);
                                }}
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
