import { useState } from 'react';
import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart as regularHeart } from '@fortawesome/free-regular-svg-icons';
import { faHeart as solidHeart } from '@fortawesome/free-solid-svg-icons';

import styles from '../Comment/Comment.module.scss';

const cx = classNames.bind(styles);

function Reply({ reply, visible }) {
    const [activeReply, setActiveReply] = useState(false);
    // const [voteCountReply, setVoteCountReply] = useState(null);
    // const [replies, setReplies] = useState([]);
    // const [reply2, setReply2] = useState({});

    let date = new Date();

    const handleVoteReply = () => {};
    // const handleSumit = (e) => {};
    // const handleSubmitReply = (e) => {};
    const handelVisible = () => setActiveReply(!activeReply);

    return (
        <div className={cx('comment-node')}>
            <div className={cx('comment__child-avt')}>
                <Link to={`/user/duyan`}>
                    <img
                        src={'https://www.gravatar.com/avatar/262cfa0997548c39953a9607a56f27da?d=wavatar&f=y'}
                        alt=""
                    />
                </Link>
            </div>
            <div className={cx('comment__child-body')}>
                <div className={cx('creator-info')}>
                    <Link to={`/user/duyan`}>
                        <span className={cx('name-main')}>Đào Duy An</span>
                    </Link>
                    <div className={cx('metadata')}>
                        <span className={cx('date')}>{`${date.getDate()}/${
                            date.getMonth() + 1
                        }/${date.getFullYear()}`}</span>
                    </div>
                    <div className={cx('comment__child-content')}>Cảm ơn góc nhìn mà chị đã chia sẻ ạ</div>
                    <div className={cx('comment__child-actions')}>
                        {/* <div className={cx('like')}>
                            <div className={cx('upvote')} onClick={() => handleVoteReply(reply._id)}>
                                <div className={cx('like-icon')}>
                                    <FontAwesomeIcon icon={solidHeart} />
                                </div>
                            </div>
                            <div></div>
                            <span className={cx('value')}>
                               {voteCountReply ? voteCountReply : reply.voteCount.length} 9
                            </span>
                        </div> */}
                        <p className={cx('reply')} onClick={handelVisible}>
                            Trả lời
                        </p>
                    </div>
                </div>
            </div>
            
        </div>
    );
}

export default Reply;
