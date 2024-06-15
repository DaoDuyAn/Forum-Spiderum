import { useEffect, useCallback, useState, useRef } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPen } from '@fortawesome/free-solid-svg-icons';

import classNames from 'classnames/bind';

import styles from './UserSettings.module.scss';

const cx = classNames.bind(styles);

function UserSettings() {
    const toast = useRef(null);

    const [isSuccess, setIsSuccess] = useState(null);
    const [isErr, setIsErr] = useState(null);
    const [disable, setDisable] = useState(true);
    const [isUpdate, setIsUpdate] = useState(false);

    const [selectedFile, setSelectedFile] = useState();
    const [selectedFileCover, setSelectedFileCover] = useState();
    const [previewCover, setPreviewCover] = useState();
    const [preview, setPreview] = useState();
    const [text, setText] = useState('');

    const [dataUser, setDataUser] = useState({});
    const [checked, setChecked] = useState(1);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userId = localStorage.getItem('userId');
                console.log('...', userId);
                const response = await axios.get(`https://localhost:44379/api/v1/GetUserById/id/${userId}`);
                setDataUser(response.data);
                setChecked(parseInt(response.data.gender));
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    const [dataPassword, setDataPassword] = useState({
        oldPassword: '',
        password: '',
        confirmPassword: '',
    });
    
    const [error, setError] = useState({
        password: '',
        confirmPassword: '',
        oldPassword: '',
    });

    const onSubmit = useCallback((e) => {
        e.preventDefault();
    }, []);

    // useEffect(() => {
    //     if (!selectedFileCover) {
    //         setPreviewCover(undefined);
    //         return;
    //     }

    //     const objectUrl = URL.createObjectURL(selectedFileCover);
    //     setPreviewCover(objectUrl);

    //     return () => URL.revokeObjectURL(objectUrl);
    // }, [selectedFileCover]);

    const fileToBase64 = async (file) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = (error) => reject(error);
        });
    };

    useEffect(() => {
        if (!selectedFileCover) {
            setPreviewCover(undefined);
            return;
        }
    
        const getBase64 = async () => {
            try {
                const base64 = await fileToBase64(selectedFileCover);
                setPreviewCover(base64);
            } catch (error) {
                console.error('Error converting file to base64:', error);
            }
        };
    
        getBase64();
    
        return () => {
            // Cleanup function
        };
    }, [selectedFileCover]);
    

    const onSelectFileCover = useCallback(
        async (e) => {

            if (!e.target.files || e.target.files.length === 0) {
                setSelectedFileCover(undefined);
                return;
            }
            setSelectedFileCover(e.target.files[0]);
        },
        [dataUser],
    );

    // useEffect(() => {
    //     if (!selectedFile) {
    //         setPreview(undefined);
    //         return;
    //     }

    //     const objectUrl = URL.createObjectURL(selectedFile);
    //     setPreview(objectUrl);

    //     return () => URL.revokeObjectURL(objectUrl);
    // }, [selectedFile]);

    useEffect(() => {
        if (!selectedFile) {
            setPreview(undefined);
            return;
        }
    
        const getBase64 = async () => {
            try {
                const base64 = await fileToBase64(selectedFile);
                setPreview(base64);
            } catch (error) {
                console.error('Error converting file to base64:', error);
            }
        };
    
        getBase64();
    
        return () => {
            // Cleanup function
        };
    }, [selectedFile]);
    

    const onSelectFile = useCallback(
        async (e) => {
            // const data = new FormData();
            // data.append('file', e.target.files[0]);
            // data.append('name', 'file');

            if (!e.target.files || e.target.files.length === 0) {
                setSelectedFile(undefined);
                return;
            }
            setSelectedFile(e.target.files[0]);
        },
        [dataUser],
    );

    const handleDesc = (e) => {
        setText(e.target.value);
    };

    const onInputChange = (e) => {
        const { name, value } = e.target;

        setDataPassword((prev) => ({
            ...prev,
            [name]: value,
        }));
        validateInput(e);
    };

    useEffect(() => {
        if (dataPassword.password !== '' && dataPassword.confirmPassword !== '' && dataPassword.oldPassword !== '') {
            if (error.password === '' && error.confirmPassword === '' && error.oldPassword === '') {
                setDisable(false);
            } else {
                setDisable(true);
            }
        }
    }, [error, dataPassword]);

    const validateInput = (e) => {
        let { name, value } = e.target;

        setError((prev) => {
            const stateObj = { ...prev, [name]: '' };

            switch (name) {
                case 'oldPassword':
                    if (value.length < 6) {
                        stateObj[name] = 'Mật khẩu phải có tối thiểu 6 kí tự và ít hơn 100 kí tự.';
                    }
                    break;
                case 'password':
                    if (dataPassword.confirmPassword && value !== dataPassword.confirmPassword) {
                        stateObj['confirmPassword'] = 'Mật khẩu nhập lại không chính xác.';
                    } else if (dataPassword.oldPassword && value === dataPassword.oldPassword) {
                        stateObj[name] = 'Mật khẩu mới không được giống mật khẩu cũ';
                    } else if (value.length < 6) {
                        stateObj[name] = 'Mật khẩu phải có tối thiểu 6 kí tự và ít hơn 100 kí tự.';
                    }
                    break;
                case 'confirmPassword':
                    if (dataPassword.password && value !== dataPassword.password) {
                        stateObj[name] = 'Mật khẩu nhập lại không chính xác';
                    }
                    break;
                default:
                    break;
            }

            return stateObj;
        });
    };

    const handleGenderChange = (gender) => {
        setChecked(gender);
        setDataUser({ ...dataUser, gender: checked });
    };

    const handelChangeData = (e) => {
        setDataUser({ ...dataUser, description: e.target.value });
    };

    const formatDate = (isoDate) => {
        if (!isoDate) return '';
        const date = new Date(isoDate);
        const day = date.getDate().toString().padStart(2, '0');
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    };

    const onSave = async (e) => {
        e.preventDefault();
        try {
            const userId = localStorage.getItem('userId');
            const accessToken = localStorage.getItem('accessToken');
  
            const payload = {
                Id: userId,
                FullName: dataUser.fullName,
                BirthDate: formatDate(dataUser.birthDate),
                Description: dataUser.description,
                Email: dataUser.email,
                Phone: dataUser.phone,
                Address: dataUser.address,
                Gender: checked,
                AvatarImagePath: preview ? preview : dataUser.avatarImagePath,
                CoverImagePath: previewCover ? previewCover : dataUser.coverImagePath
            };
    
            const response = await axios.put(
                'https://localhost:44379/api/v1/UpdateProfile',
                payload,
                {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                }
            );
    
            if (response.status === 200) {
                setIsSuccess('Cập nhật thông tin thành công.');
                setIsErr(null);
            } else {
                setIsErr('Có lỗi xảy ra khi cập nhật thông tin.');
                setIsSuccess(null);
            }
        } catch (error) {
            setIsErr('Có lỗi xảy ra khi cập nhật thông tin.');
            setIsSuccess(null);
            console.error('Error updating profile:', error);
        }
    };
    
    const onSubmitPassword = async (e) => {
        e.preventDefault();
        try {
            const userId = localStorage.getItem('userId');
            const accessToken = localStorage.getItem('accessToken');
            const response = await axios.put(
                'https://localhost:44379/api/v1/ChangePassword',
                {
                    oldPassword: dataPassword.oldPassword,
                    newPassword: dataPassword.password,
                    confirmPassword: dataPassword.confirmPassword,
                    userId: userId,
                },
                {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                },
            );

            if (response.data === 1) {
                setIsSuccess('Cập nhật mật khẩu thành công.');
                setIsErr(null);
            } else {
                setIsErr('Có lỗi xảy ra khi cập nhật mật khẩu.');
                setIsSuccess(null);
            }
        } catch (error) {
            setIsErr('Có lỗi xảy ra khi cập nhật mật khẩu.');
            setIsSuccess(null);
            console.error('Error updating password:', error);
        }
    };

    useEffect(() => {
        document.title = `Cài đặt người dùng`;
    }, []);

    return (
        <div className={cx('container')}>
            <div className={cx('mt-190')}>
                <div className={cx('settings')}>
                    {isErr || isSuccess ? (
                        <div className="toast-mess-container">
                            <button ref={toast} className={cx('alert-toast-message', { err: isErr, success: !isErr })}>
                                {isErr ? isErr : isSuccess}
                            </button>
                        </div>
                    ) : (
                        ''
                    )}

                    <div className={cx('grid')}>
                        <div className={cx('grid', 'grid-cols-12')}>
                            <div className={cx('col-span-3')}>
                                <ul className={cx('settings__navbar')}>
                                    <li className={cx('settings__navbar-item')}>
                                        <span className={cx('settings__navbar-text')}>Tài khoản</span>
                                    </li>
                                </ul>
                            </div>
                            <div className={cx('col-span-9')}>
                                <form action="" method="PUT" onSubmit={onSubmit}>
                                    <div className={cx('settings__content')}>
                                        <div className={cx('settings__cover')}>
                                            <form multiple action="" className={cx('settings__cover-container')}>
                                                {previewCover ? (
                                                    <img
                                                        src={previewCover}
                                                        alt=""
                                                        className={cx('settings__avt-img', 'cover')}
                                                    />
                                                ) : (
                                                    <img
                                                        src={dataUser.coverImagePath}
                                                        alt=""
                                                        className={cx('settings__avt-img', 'cover')}
                                                    />
                                                )}
                                                <label className={cx('settings__avt-icon', 'cover')}>
                                                    <div className={cx('settings__avt-icon-upload')}>
                                                        <svg
                                                            height="40"
                                                            viewBox="0 0 1000 1000"
                                                            width="40"
                                                            x="0px"
                                                            y="0px"
                                                            className={cx('ng-tns-c79-0', 'ng-star-inserted')}
                                                        >
                                                            <g
                                                                _ngcontent-serverApp-c79=""
                                                                fill="#FFF"
                                                                className={cx('ng-tns-c79-0')}
                                                            >
                                                                <path
                                                                    _ngcontent-serverApp-c79=""
                                                                    d="M336.7,91.7h326.7L745,214.2h122.5c33.8,0,62.7,12.2,86.6,36.5s35.9,53.4,35.9,87.2v448.5c0,33.8-12,62.6-35.9,86.3s-52.8,35.6-86.6,35.6h-735c-33.8,0-62.7-12-86.6-35.9S10,819.6,10,785.8V337.3c0-33.8,12-62.8,35.9-86.9s52.8-36.2,86.6-36.2H255L336.7,91.7z M500,336.7c27.6,0,54.1,5.4,79.3,16.3c25.2,10.8,46.9,25.4,65.1,43.5s32.7,39.9,43.5,65.1c10.8,25.2,16.3,51.6,16.3,79.3c0,27.6-5.4,54.1-16.3,79.3c-10.8,25.2-25.4,46.9-43.5,65.1s-39.9,32.7-65.1,43.5C554.1,739.6,527.6,745,500,745s-54.1-5.4-79.3-16.3c-25.2-10.8-46.9-25.4-65.1-43.5s-32.7-39.9-43.5-65.1s-16.3-51.6-16.3-79.3c0-27.6,5.4-54.1,16.3-79.3c10.8-25.2,25.4-46.9,43.5-65.1s39.9-32.7,65.1-43.5C445.9,342.1,472.4,336.7,500,336.7z M500,418.3c-33.8,0-62.7,12-86.6,35.9s-35.9,52.8-35.9,86.6c0,33.8,12,62.7,35.9,86.6s52.8,35.9,86.6,35.9c33.8,0,62.7-12,86.6-35.9s35.9-52.8,35.9-86.6c0-33.8-12-62.7-35.9-86.6S533.8,418.3,500,418.3z M701.3,295.8l-80.1-122.5H380.4l-81.7,122.5H132.5c-11.3,0-20.9,4-28.9,12.1c-8,8.1-12,17.9-12,29.3v448.5c0,11.3,4,20.9,12,28.9c8,8,17.6,12,28.9,12h735c11.5,0,21.2-3.9,29-11.6c7.9-7.8,11.8-17.3,11.8-28.6V337.9c0-11.5-4-21.4-12.1-29.7c-8.1-8.3-17.7-12.4-28.7-12.4L701.3,295.8L701.3,295.8z"
                                                                    className={cx('ng-tns-c79-0')}
                                                                ></path>
                                                            </g>
                                                        </svg>
                                                        <input
                                                            type="file"
                                                            className={cx('settings__avt-input')}
                                                            onChange={onSelectFileCover}
                                                        />
                                                    </div>
                                                </label>
                                            </form>
                                        </div>
                                        <div className={cx('settings__account')}>
                                            <div className={cx('settings__flex-top')}>
                                                <form className={cx('settings__flex-30', 'settings__avt')}>
                                                    {preview ? (
                                                        <img src={preview} alt="" className={cx('settings__avt-img')} />
                                                    ) : (
                                                        <img
                                                            src={dataUser.avatarImagePath}
                                                            alt=""
                                                            className={cx('settings__avt-img')}
                                                        />
                                                    )}
                                                    <label className={cx('settings__avt-icon')}>
                                                        <div className={cx('settings__avt-icon-upload')}>
                                                            <svg
                                                                height="40"
                                                                viewBox="0 0 1000 1000"
                                                                width="40"
                                                                x="0px"
                                                                y="0px"
                                                                className=""
                                                            >
                                                                <g fill="#FFF">
                                                                    <path d="M336.7,91.7h326.7L745,214.2h122.5c33.8,0,62.7,12.2,86.6,36.5s35.9,53.4,35.9,87.2v448.5c0,33.8-12,62.6-35.9,86.3s-52.8,35.6-86.6,35.6h-735c-33.8,0-62.7-12-86.6-35.9S10,819.6,10,785.8V337.3c0-33.8,12-62.8,35.9-86.9s52.8-36.2,86.6-36.2H255L336.7,91.7z M500,336.7c27.6,0,54.1,5.4,79.3,16.3c25.2,10.8,46.9,25.4,65.1,43.5s32.7,39.9,43.5,65.1c10.8,25.2,16.3,51.6,16.3,79.3c0,27.6-5.4,54.1-16.3,79.3c-10.8,25.2-25.4,46.9-43.5,65.1s-39.9,32.7-65.1,43.5C554.1,739.6,527.6,745,500,745s-54.1-5.4-79.3-16.3c-25.2-10.8-46.9-25.4-65.1-43.5s-32.7-39.9-43.5-65.1s-16.3-51.6-16.3-79.3c0-27.6,5.4-54.1,16.3-79.3c10.8-25.2,25.4-46.9,43.5-65.1s39.9-32.7,65.1-43.5C445.9,342.1,472.4,336.7,500,336.7z M500,418.3c-33.8,0-62.7,12-86.6,35.9s-35.9,52.8-35.9,86.6c0,33.8,12,62.7,35.9,86.6s52.8,35.9,86.6,35.9c33.8,0,62.7-12,86.6-35.9s35.9-52.8,35.9-86.6c0-33.8-12-62.7-35.9-86.6S533.8,418.3,500,418.3z M701.3,295.8l-80.1-122.5H380.4l-81.7,122.5H132.5c-11.3,0-20.9,4-28.9,12.1c-8,8.1-12,17.9-12,29.3v448.5c0,11.3,4,20.9,12,28.9c8,8,17.6,12,28.9,12h735c11.5,0,21.2-3.9,29-11.6c7.9-7.8,11.8-17.3,11.8-28.6V337.9c0-11.5-4-21.4-12.1-29.7c-8.1-8.3-17.7-12.4-28.7-12.4L701.3,295.8L701.3,295.8z"></path>
                                                                </g>
                                                            </svg>
                                                            <input
                                                                type="file"
                                                                className={cx('settings__avt-input')}
                                                                onChange={onSelectFile}
                                                            />
                                                        </div>
                                                    </label>
                                                </form>
                                                <div className={cx('settings__flex-70')}>
                                                    <textarea
                                                        className={cx('settings__textarea')}
                                                        value={dataUser.description}
                                                        onChange={(e) => {
                                                            handleDesc(e);
                                                            handelChangeData(e);
                                                        }}
                                                    ></textarea>
                                                    <p
                                                        className={cx('settings__textarea-length', {
                                                            red: text.length >= 150,
                                                        })}
                                                    >
                                                        {text.length}/150
                                                    </p>
                                                </div>
                                            </div>
                                            <div className={cx('settings__flex')}>
                                                <div className={cx('settings__flex-item')}>
                                                    <label className={cx('settings__name')}>TÊN HIỂN THỊ</label>
                                                    <input
                                                        type="text"
                                                        className={cx('settings__input')}
                                                        value={dataUser.fullName}
                                                        onChange={(e) =>
                                                            setDataUser({
                                                                ...dataUser,
                                                                fullName: e.target.value,
                                                            })
                                                        }
                                                    />
                                                </div>
                                                <div className={cx('settings__flex-item')}>
                                                    <div>
                                                        <label className={cx('settings__name')}>EMAIL</label>
                                                        <div className={cx('inputContainer')}>
                                                            <input
                                                                type="text"
                                                                readOnly
                                                                className={cx('settings__input', 'input-field')}
                                                                value={dataUser.email}
                                                                onClick={() => setIsUpdate(true)}
                                                            />
                                                            <FontAwesomeIcon
                                                                className={cx('input-icon')}
                                                                icon={faPen}
                                                                onClick={() => setIsUpdate(true)}
                                                            />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className={cx('settings__flex')}>
                                                <div className={cx('settings__flex-item')}>
                                                    <label className={cx('settings__name')}>NGÀY SINH</label>
                                                    <input
                                                        type="date"
                                                        className={cx('settings__input')}
                                                        value={
                                                            dataUser.birthDate
                                                                ? new Date(dataUser.birthDate)
                                                                      .toISOString()
                                                                      .substr(0, 10)
                                                                : ''
                                                        }
                                                        onChange={(e) =>
                                                            setDataUser({
                                                                ...dataUser,
                                                                birthDate: e.target.value,
                                                            })
                                                        }
                                                    />
                                                </div>
                                                <div className={cx('settings__flex-item')}>
                                                    <label className={cx('settings__name')}>GIỚI TÍNH</label>
                                                    <div className={cx('flex', 'flex-wrap', 'p-[12px]')}>
                                                        <div className={cx('flex', 'items-center', 'mb-4', 'mr-7')}>
                                                            <input
                                                                type="radio"
                                                                className={cx('hidden')}
                                                                checked={checked === 1}
                                                            />
                                                            <label
                                                                className={cx('flex', 'items-center', 'cursor-pointer')}
                                                                onClick={() => handleGenderChange(1)}
                                                            >
                                                                <span
                                                                    className={cx(
                                                                        'w-6',
                                                                        'h-6',
                                                                        'inline-block',
                                                                        'mr-2',
                                                                        'rounded-full',
                                                                        'border',
                                                                        'border-grey',
                                                                    )}
                                                                ></span>
                                                                Nam
                                                            </label>
                                                        </div>
                                                        <div className={cx('flex', 'items-center', 'mb-4', 'mr-5')}>
                                                            <input
                                                                type="radio"
                                                                className={cx('hidden')}
                                                                checked={checked === 0}
                                                            />
                                                            <label
                                                                className={cx('flex', 'items-center', 'cursor-pointer')}
                                                                onClick={() => handleGenderChange(0)}
                                                            >
                                                                <span
                                                                    className={cx(
                                                                        'w-6',
                                                                        'h-6',
                                                                        'inline-block',
                                                                        'mr-2',
                                                                        'rounded-full',
                                                                        'border',
                                                                        'border-grey',
                                                                    )}
                                                                ></span>
                                                                Nữ
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className={cx('settings__form')}>
                                                <p className={cx('settings__password')}>Đổi mật khẩu</p>
                                                <form className={cx('settings__flex')} onSubmit={onSubmit}>
                                                    <div className={cx('settings__flex-item-wfull')}>
                                                        <label className={cx('settings__name')} htmlFor="">
                                                            Mật khẩu cũ
                                                        </label>
                                                        <input
                                                            type="password"
                                                            name="oldPassword"
                                                            className={cx('settings__input', {
                                                                wrong: error.oldPassword,
                                                            })}
                                                            placeholder="****************************************"
                                                            value={dataPassword.oldPassword}
                                                            onChange={onInputChange}
                                                            onBlur={validateInput}
                                                        />
                                                        {error.oldPassword && (
                                                            <p className={cx('settings__mess')}>{error.oldPassword}</p>
                                                        )}
                                                    </div>
                                                    <div className={cx('settings__flex-item-wfull')}>
                                                        <label className={cx('settings__name')}>Mật khẩu mới</label>
                                                        <input
                                                            type="password"
                                                            name="password"
                                                            className={cx('settings__input', { wrong: error.password })}
                                                            placeholder="****************************************"
                                                            value={dataPassword.password}
                                                            onChange={onInputChange}
                                                            onBlur={validateInput}
                                                        />
                                                        {error.password && (
                                                            <p className={cx('settings__mess')}>{error.password}</p>
                                                        )}
                                                    </div>
                                                    <div className={cx('settings__flex-item-wfull')}>
                                                        <label className={cx('settings__name')} htmlFor="">
                                                            Nhập lại mật khẩu mới
                                                        </label>
                                                        <input
                                                            type="password"
                                                            name="confirmPassword"
                                                            className={cx('settings__input', {
                                                                wrong: error.confirmPassword,
                                                            })}
                                                            placeholder="****************************************"
                                                            value={dataPassword.confirmPassword}
                                                            onChange={onInputChange}
                                                            onBlur={validateInput}
                                                        />
                                                        {error.confirmPassword && (
                                                            <p className={cx('settings__mess')}>
                                                                {error.confirmPassword}
                                                            </p>
                                                        )}
                                                    </div>
                                                    <button
                                                        type="submit"
                                                        disabled={disable}
                                                        onClick={(e) => onSubmitPassword(e)}
                                                        className={cx('settings__button', { active: !disable })}
                                                    >
                                                        Xác nhận
                                                    </button>
                                                </form>
                                            </div>
                                            <div className={cx('settings__flex')}>
                                                <div className={cx('settings__flex-item')}>
                                                    <label htmlFor="" className={cx('settings__name')}>
                                                        Địa chỉ
                                                    </label>
                                                    <input
                                                        type="text"
                                                        className={cx('settings__input')}
                                                        value={dataUser.address}
                                                        onChange={(e) =>
                                                            setDataUser({
                                                                ...dataUser,
                                                                address: e.target.value,
                                                            })
                                                        }
                                                    />
                                                </div>
                                                <div className={cx('settings__flex-item')}>
                                                    <label htmlFor="" className={cx('settings__name')}>
                                                        Số điện thoại
                                                    </label>
                                                    <input
                                                        type="number"
                                                        className={cx('settings__input')}
                                                        value={dataUser.phone}
                                                        onChange={(e) =>
                                                            setDataUser({
                                                                ...dataUser,
                                                                phone: e.target.value,
                                                            })
                                                        }
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className={cx('settings__actions')}>
                                            <button className={cx('settings__actions', 'cancle')}>
                                                <Link to="/">Hủy</Link>
                                            </button>

                                            <button
                                                type="submit"
                                                className={cx('settings__actions', 'save')}
                                                onClick={onSave}
                                            >
                                                Lưu
                                            </button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default UserSettings;
