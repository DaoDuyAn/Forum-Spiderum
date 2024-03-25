import { useState } from 'react';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart as regularHeart } from '@fortawesome/free-regular-svg-icons';
import { faHeart as solidHeart } from '@fortawesome/free-solid-svg-icons';

import Reply from '../Reply';
import styles from './Comment.module.scss';

const cx = classNames.bind(styles);

function Comment({ comment, postId }) {
    const [likeCount, setLikeCount] = useState(null);
    const [activeReply, setActiveReply] = useState(false);
    const [replies, setReplies] = useState([]);
    const [reply, setReply] = useState({});

    let date = new Date();

    const handleLike = () => {};
    const handelVisible = () => setActiveReply(!activeReply);
    const handleSumit = (e) => {};
    const handleSubmitReply = (e) => {};

    return (
        <div className={cx('comment__child')}>
            <div className={cx('comment__child-avt')}>
                <Link to={`/user/duyan`}>
                    <img
                        src={'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'}
                        alt="ava_img"
                    />
                </Link>
            </div>
            <div className={cx('comment__child-body')}>
                <div className={cx('creator-info')}>
                    <Link to={`/user/duyan`}>
                        <span className={cx('name-main')}>Châu Anh Kiệt</span>
                    </Link>
                    <div className={cx('metadata')}>
                        <span className={cx('date')}>{`${date.getDate()}/${
                            date.getMonth() + 1
                        }/${date.getFullYear()}`}</span>
                    </div>
                    <div className={cx('comment__child-content')}>Trở lại mạnh mẽ quá nhỉ</div>
                    <div className={cx('comment__child-actions')}>
                        {/* <div className={cx('like')}>
                            <div className={cx('upvote')} onClick={handleLike}>
                                <div className={cx('like-icon')}>
                                    <FontAwesomeIcon icon={regularHeart} />
                                </div>
                            </div>
                            <div></div>
                            <span className={cx('value')}>5 {likeCount}</span>
                        </div> */}
                        <p className={cx('reply')} onClick={handelVisible}>
                            Trả lời
                        </p>
                    </div>
                </div>
            </div>
            {activeReply ? (
                <div className={cx('action-reply')}>
                    <div className={cx('reply-comment')}>
                        <div className={cx('reply-comment-form')}>
                            <form action="" className={cx('comment__form')} onSubmit={handleSumit}>
                                <input
                                    className={cx('comment__form-data')}
                                    placeholder="Cảm nghĩ của bạn về comment này"
                                    value={reply.content}
                                    onChange={(e) => setReply({ ...reply, content: e.target.value })}
                                ></input>
                                <div className={cx('comment__form-actions')} onClick={handleSubmitReply}>
                                    <div className={cx('comment__form-actions-submit')}>Trả lời</div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            ) : (
                ''
            )}
            <div className={cx('comments-reply')}>
                {/* {replies?.map((reply) => (
                    <Reply reply={reply} visible={handelVisible} key={reply._id} />
                ))} */}
                <>
                    <Reply reply={reply} visible={handelVisible} key={1} />
                    <Reply reply={reply} visible={handelVisible} key={1} />

                </>
            </div>
        </div>
    );
}

export default Comment;
