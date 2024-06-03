import { useState, useEffect } from 'react';
import axios from 'axios';
import { Link, useSearchParams, useNavigate } from 'react-router-dom';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faFile } from '@fortawesome/free-solid-svg-icons';

import PostItem from '~/components/PostItem';
import UsersSearch from '~/components/UsersSearch';
import Pagination from '@mui/material/Pagination';

import styles from './Search.module.scss';
import { styled } from '@mui/system';

const StyledPagination = styled(Pagination)({
    '& .MuiPaginationItem-root': {
        fontSize: '1.5rem',
    },
});

const cx = classNames.bind(styles);

function Search() {
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();

    const [posts, setPosts] = useState([]);
    const [users, setUsers] = useState([]);
 
    const [pageIdx, setPageIdx] = useState(1);
    const [pageCount, setPageCount] = useState(1);

    const query = searchParams.get('q');
    const type = searchParams.get('type') || 'post';

    // useEffect(() => {
    //     const fetchPosts = async () => {
    //         try {
    //             const response = await axios.get('https://localhost:44379/api/v1/GetAllPosts');
    //             setPosts(response.data);
    //         } catch (error) {
    //             console.error('Error fetching posts:', error);
    //         }
    //     };

    //     fetchPosts();
    // }, []);

    useEffect(() => {
        const fetchData = async () => {
            try {
                if (type === 'post') {
                    const response = await axios.get(
                        `https://localhost:44379/api/v1/SearchPostByValue?q=${query}&page=${pageIdx}`,
                    );
                    setPosts(response.data.postResponse);
                    setPageCount(response.data.pageCount);
                } else if (type === 'user') {
                    const response = await axios.get(
                        `https://localhost:44379/api/v1/SearchUserByValue?q=${query}&page=${pageIdx}`,
                    );
                    setUsers(response.data.userResponse);
                    setPageCount(response.data.pageCount);
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [query, type, pageIdx]);


    useEffect(() => {
        setPageIdx(1);
    }, [type]);

    const handlePageIdxChange = (newPageIdx) => {
        setPageIdx(newPageIdx);
    };

    return (
        <div className={cx('search')}>
            <div className={cx('search__container')}>
                <div className={cx('search__heading')}>
                    <h3 className={cx('search__heading-name')}>
                        Kết quả tìm kiếm:
                        <span> "{query}"</span>
                    </h3>
                </div>
                <div className={cx('search__content')}>
                    <div className={cx('search__content-body')}>
                        <div className={cx('search__content-nav')}>
                            <ul className={cx('search__content-nav-menu')}>
                                <li
                                    className={cx('search__content-nav-item')}
                                    onClick={() => navigate(`/search?q=${query}&type=post`)}
                                >
                                    <div
                                        className={cx('search__content-nav-item-wrapper', { active: type === 'post' })}
                                    >
                                        <Link to="/" className={cx('search__content-nav-link')}>
                                            <FontAwesomeIcon icon={faFile} className={cx('search__content-nav-icon')} />
                                            <span className={cx('search__content-nav-text')}>Bài viết</span>
                                        </Link>
                                    </div>
                                </li>
                                <li
                                    className={cx('search__content-nav-item')}
                                    onClick={() => navigate(`/search?q=${query}&type=user`)}
                                >
                                    <div
                                        className={cx('search__content-nav-item-wrapper', { active: type === 'user' })}
                                    >
                                        <Link to="/" className={cx('search__content-nav-link')}>
                                            <FontAwesomeIcon className={cx('search__content-nav-icon')} icon={faUser} />
                                            <span className={cx('search__content-nav-text')}>Người dùng</span>
                                        </Link>
                                    </div>
                                </li>
                            </ul>
                        </div>

                        {/* Search result */}
                        <div className={cx('search__content-result')}>
                            {type === 'post' ? (
                                <>{posts && posts.map((post) => <PostItem key={post._id} post={post} />)}</>
                            ) : (
                                <div className={cx('user-container')}>
                                    <>{users && users.map((user, idx) => <UsersSearch key={idx} user={user} />)}</>
                                </div>
                            )}
                        </div>
                    </div>
                </div>

                <div className={cx('flex', 'justify-center', 'm-[30px]')}>
                    <StyledPagination
                        count={pageCount}
                        page={pageIdx} // Trang hiện tại
                        onChange={(event, page) => handlePageIdxChange(page)}
                        variant="outlined"
                        color="primary"
                        size="large"
                        boundaryCount={2}
                        showFirstButton
                        showLastButton
                    />
                </div>
            </div>
        </div>
    );
}

export default Search;
