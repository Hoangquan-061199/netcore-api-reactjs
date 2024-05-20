import { Button, Modal } from 'antd';
import { ExclamationCircleFilled } from '@ant-design/icons';

const { confirm } = Modal;

const DeleteAccount = () => {
    const showConfirm = () => {
        confirm({
            title: 'Bạn chắc chắn muốn xoá tài khoản chứ',
            icon: <ExclamationCircleFilled />,
            content: 'Xoá tài khoản bạn sẽ không thể đăng nhập vào trang quản trị :(',
            onOk() {
                console.log('OK');
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    };
    return (
        <>
            <Button onClick={showConfirm} danger>
                Xoá tài khoản
            </Button>
        </>
    );
};

export default DeleteAccount;
