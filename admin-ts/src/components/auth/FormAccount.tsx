import { Avatar, Button, Col, Form, Input, Row, Upload } from 'antd';
import { useSelector } from 'react-redux';
import { selectCurrentUserId } from '../../redux/users/user.slice';
import { useGetUserUpdateQuery } from '../../redux/users/user.service';

const FormAccount = () => {
    const [form] = Form.useForm();
    const userId = useSelector(selectCurrentUserId);
    const { data } = useGetUserUpdateQuery();

    const onFinish = (values: any) => {
        console.log('Received values of form: ', values);
    };

    const initValueForm = [
        {
            name: 'fullName',
            value: data?.fullName,
        },
        {
            name: 'email',
            value: data?.email,
        },
        {
            name: 'userName',
            value: data?.userName,
        },
        {
            name: 'departmentName',
            value: data?.departmentName,
        },
        {
            name: 'roles',
            value: data?.roles,
        },
        {
            name: 'createdDate',
            value: data?.createdDate,
        },
        {
            name: 'urlPicture',
            value: data?.urlPicture,
        },
    ];

    return (
        <div>
            <Form
                layout="vertical"
                form={form}
                fields={initValueForm}
                name="updateaccount"
                onFinish={onFinish}
                scrollToFirstError>
                <input type="hidden" name="userId" value={userId} />
                <Row gutter={16}>
                    <Col className="gutter-row" span={4}>
                        <div style={{ height: 'auto', width: '80%', aspectRatio: 1 }}>
                            <Form.Item>
                                <Form.Item
                                    name="urlPicture"
                                    valuePropName="file"
                                    style={{ borderRadius: '50%', width: '100%' }}>
                                    <Upload.Dragger name="files" action="" maxCount={1}>
                                        <Avatar src={<img src={data?.urlPicture} alt="avatar" />} />
                                    </Upload.Dragger>
                                </Form.Item>
                            </Form.Item>
                        </div>
                    </Col>
                    <Col className="gutter-row" span={20}>
                        <Row gutter={16}>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="userName" label="Tài khoản:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="departmentName" label="Phòng ban:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="roles" label="Quyền:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="createdDate" label="Ngày tạo:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item
                                    name="fullName"
                                    label="Họ tên:"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'Vui lòng nhập họ tên',
                                        },
                                    ]}>
                                    <Input />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item
                                    name="email"
                                    label="Email:"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'Vui lòng nhập Email',
                                        },
                                        {
                                            type: 'email',
                                            message: 'Email không hợp lệ',
                                        },
                                    ]}>
                                    <Input />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item>
                                    <Button type="primary" htmlType="submit">
                                        Cập nhật
                                    </Button>
                                </Form.Item>
                            </Col>
                        </Row>
                    </Col>
                </Row>
            </Form>
        </div>
    );
};

export default FormAccount;
