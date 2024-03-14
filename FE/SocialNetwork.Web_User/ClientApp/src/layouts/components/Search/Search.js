import { useEffect, useState, useRef } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleXmark, faSpinner, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import classNames from 'classnames/bind';

// import * as searchServices from '~/services/searchService';
import styles from './Search.module.scss';

const cx = classNames.bind(styles);

function Search() {
    const [searchValue, setSearchValue] = useState('');
  

    const [loading, setLoading] = useState(false);

    const inputRef = useRef();

    useEffect(() => {
        if (!searchValue.trim()) {
            return;
        }

        const fetchApi = async () => {
            setLoading(true);

            // const result = await searchServices.search(searchValue);

            setLoading(false);
        };

        fetchApi();
    }, [searchValue]);

    const handleClear = () => {
        setSearchValue('');
        
        inputRef.current.focus();
    };

    const handleChange = (e) => {
        const searchValue = e.target.value;
        if (!searchValue.startsWith(' ')) {
            setSearchValue(searchValue);
        }
    };

    return (
        // Using a wrapper <div> or <span> tag around the reference element solves this
        // by creating a new parentNode context.
        <div>
            <div className={cx('search')}>
                <input
                    ref={inputRef}
                    value={searchValue}
                    placeholder="Tìm kiếm theo chủ đề hoặc tên người dùng"
                    spellCheck={false}
                    onChange={handleChange}
                />
                {!!searchValue && !loading && (
                    <button className={cx('clear')} onClick={handleClear}>
                        <FontAwesomeIcon icon={faCircleXmark} />
                    </button>
                )}
                {loading && <FontAwesomeIcon className={cx('loading')} icon={faSpinner} />}
                <button className={cx('search-btn')} onMouseDown={(e) => e.preventDefault()}>
                <FontAwesomeIcon icon={faMagnifyingGlass} />
                </button>
            </div>
        </div>
    );
}

export default Search;
