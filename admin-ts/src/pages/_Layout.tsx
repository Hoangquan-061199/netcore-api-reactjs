import { useSelector } from 'react-redux';
import { Navigate, Outlet } from 'react-router-dom';
import { selectCurrentToken } from '../redux/auth/Auth.slice';
import { Layout, theme } from 'antd';
import Sidebar from '../components/Sidebar';
import { useState } from 'react';
import HeaderComponent from '../components/Header';
import FooterComponent from '../components/Footer';

const { Content } = Layout;

const _Layout = () => {
    const istoken = useSelector(selectCurrentToken);
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer },
    } = theme.useToken();
    return istoken ? (
        <>
            <Layout style={{ height: '100vh', overflow: 'hidden' }}>
                <Sidebar collapsed={collapsed} />
                <Layout>
                    <HeaderComponent
                        collapsed={collapsed}
                        colorBgContainer={colorBgContainer}
                        setCollapsed={setCollapsed}
                    />
                    <Content
                        style={{
                            margin: '24px 16px 0',
                            background: colorBgContainer,
                        }}>
                        <div
                            style={{
                                minHeight: 360,
                                height: '100%',
                                maxHeight: 'calc(100vh - 152px)',
                                overflowY: 'auto',
                            }}>
                            <Outlet />
                        </div>
                    </Content>
                    <FooterComponent />
                </Layout>
            </Layout>
        </>
    ) : (
        <Navigate to="/login" replace={true} />
    );
};

export default _Layout;
