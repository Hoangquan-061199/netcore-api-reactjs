import { Button, message, Modal } from 'antd';
import { ExclamationCircleFilled } from '@ant-design/icons';
import { useDeleteAccountMutation, useLogoutMutation } from '../../redux/auth/Auth.service';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { logout } from '../../redux/auth/Auth.slice';

const { confirm } = Modal;

const DeleteAccount = () => {
    const [deleteAccApi, { isLoading }] = useDeleteAccountMutation();
    const [logoutApi] = useLogoutMutation();
    const dispath = useDispatch();
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            let logoutrs: any = await logoutApi();
            if (logoutrs.error && logoutrs.error.status === 400) {
                message.error(logoutrs.error.data.message);
            } else {
                message.success(logoutrs.data.message);
                dispath(logout());
                navigate('/login');
            }
        } catch (e: any) {
            console.log(e);
            message.error('Đăng xuất tài khoản thất bại :)');
        }
    };

    const showConfirm = () => {
        confirm({
            title: 'Bạn chắc chắn muốn xoá tài khoản chứ',
            icon: <ExclamationCircleFilled />,
            content: 'Xoá tài khoản bạn sẽ không thể đăng nhập vào trang quản trị :(',
            onOk: async () => {
                console.log('OK');
                try {
                    let rs: any = await deleteAccApi();
                    if (rs.error) {
                        message.error(rs.data.message);
                    } else {
                        message.success(rs.data.message);
                        await handleLogout();
                    }
                } catch (e: any) {
                    console.log(e);
                    message.error('Xoá tài khoản thất bại');
                }
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    };
    return (
        <>
            <Button onClick={showConfirm} loading={isLoading} danger>
                Xoá tài khoản
            </Button>
        </>
    );
};

export default DeleteAccount;
