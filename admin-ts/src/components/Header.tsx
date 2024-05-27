import { Avatar, Button, Dropdown, MenuProps, message, Skeleton } from 'antd';
import { Header } from 'antd/es/layout/layout';
import { MenuFoldOutlined, MenuUnfoldOutlined, UserOutlined, LogoutOutlined } from '@ant-design/icons';

import flagVi from '../assets/images/flag-vi.webp';
import flagEn from '../assets/images/flag-en.png';
import avatarImg from '../assets/images/avatar.png';
import { useLogoutMutation } from '../redux/auth/Auth.service';
import { useDispatch } from 'react-redux';
import { logout } from '../redux/auth/Auth.slice';
import { Link, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useGetUserHeaderQuery } from '../redux/users/user.service';
import { userHeader } from '../redux/users/user.slice';
import { getImageDomain } from '../helpers/utilities';

type props = {
    collapsed: boolean;
    setCollapsed: React.Dispatch<React.SetStateAction<boolean>>;
    colorBgContainer: string;
};

const HeaderComponent = ({ collapsed, setCollapsed, colorBgContainer }: props) => {
    const [logoutApi] = useLogoutMutation();
    const { data, isLoading } = useGetUserHeaderQuery();
    const dispath = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (data) {
            dispath(userHeader(data));
        }
    }, [data]);

    console.log(isLoading)
    const handleLogout = async () => {
        try {
            let rs: any = await logoutApi();
            if (rs.error && rs.error.status === 400) {
                message.error(rs.error.data.message);
            } else {
                message.success(rs.data.message);
                dispath(logout());
                navigate('/login');
            }
        } catch (e) {
            console.log(e);
            message.error('Đăng xuất thất bại!');
        }
    };

    const items: MenuProps['items'] = [
        {
            key: '1',
            icon: <UserOutlined />,
            label: (
                <Link title="Tài khoản" to="/account-admin">
                    Tài khoản
                </Link>
            ),
        },
        {
            key: '2',
            icon: <LogoutOutlined />,
            label: <div onClick={() => handleLogout()}>Đăng xuất</div>,
        },
    ];
    const items2: MenuProps['items'] = [
        {
            key: '/flag-vi',
            icon: <Avatar src={<img src={flagVi} alt="Tiếng Việt" />} />,
            label: <div title="Tiếng Việt">Tiếng Việt</div>,
        },
        {
            key: '/Flag-en',
            icon: <Avatar src={<img src={flagEn} alt="Tiếng Anh" />} />,
            label: <div title="Tiếng Anh">Tiếng Anh</div>,
        },
    ];

    return (
        <div>
            <Header
                style={{
                    padding: 0,
                    background: colorBgContainer,
                    display: 'flex',
                    justifyContent: 'flex-end',
                    alignItems: 'center',
                    paddingRight: 25,
                    gap: 15,
                }}>
                <Button
                    type="text"
                    icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                    onClick={() => setCollapsed(!collapsed)}
                    style={{
                        fontSize: '16px',
                        width: 64,
                        height: 64,
                        marginRight: 'auto',
                    }}
                />
                <Dropdown menu={{ items: items2 }} placement="bottomRight">
                    <div style={{ cursor: 'pointer' }}>
                        <Avatar shape="square" size="small" src={<img src={flagVi} alt="Tiếng Việt" />} />
                    </div>
                </Dropdown>

                <Dropdown menu={{ items: items }} placement="bottomRight">
                    <div style={{ display: 'flex', height: 64, gap: 15, alignItems: 'center', cursor: 'pointer' }}>
                        {isLoading ? (
                            <Skeleton.Avatar active={true} size="large" shape="circle" />
                        ) : (
                            <Avatar
                                size="large"
                                style={{ backgroundColor: '#d6e4ff' }}
                                icon={<UserOutlined />}
                                src={<img src={getImageDomain(data?.urlPicture, avatarImg)} alt={data?.fullName} />}
                            />
                        )}
                        <span style={{ lineHeight: 1.5 }}>
                            {isLoading ? (
                                <Skeleton active={true} />
                            ) : (
                                <span style={{ lineHeight: 1.5, display: 'block', fontWeight: 'bold', fontSize: 18 }}>
                                    {data?.fullName}
                                </span>
                            )}
                            {isLoading ? (
                                <Skeleton active={true} />
                            ) : (
                                <span style={{ lineHeight: 1.5 }}>{data?.roles}</span>
                            )}
                        </span>
                    </div>
                </Dropdown>
            </Header>
        </div>
    );
};

export default HeaderComponent;
