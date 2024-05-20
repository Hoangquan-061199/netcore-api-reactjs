import { Tabs, TabsProps } from 'antd';
import FormAccount from '../../components/auth/FormAccount';
import FormChangePassword from '../../components/auth/FormChangePassword';
import DeleteAccount from '../../components/auth/DeleteAccount';

const Account = () => {
    document.title = 'Tài khoản';

    const items: TabsProps['items'] = [
        {
            key: '1',
            label: 'Tài khoản',
            children: <FormAccount />,
            style: {padding: 24}
        },
        {
            key: '2',
            label: 'Đổi mật khẩu',
            children: <FormChangePassword />,
            style: {padding: 24}
        },
        {
            key: '3',
            label: 'Xoá tài khoản',
            children: <DeleteAccount />,
            style: {padding: 24}
        },
    ];
    return (
        <>
            <Tabs defaultActiveKey="1" items={items} tabPosition={'top'} tabBarStyle={{position: 'sticky', top: 0, zIndex: 10, padding: '0 24px', background: '#fff'}}/>
        </>
    );
};

export default Account;
