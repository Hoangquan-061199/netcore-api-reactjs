import { Button, Form, Input } from 'antd';

const FormChangePassword = () => {
    const [form] = Form.useForm();

    const onFinish = (values: any) => {
        console.log('Received values of form: ', values);
    };

    return (
        <div>
            <Form
                layout="vertical"
                form={form}
                style={{ maxWidth: 600 }}
                name="changePassword"
                onFinish={onFinish}
                scrollToFirstError>
                <Form.Item
                    label="Mật khẩu cũ"
                    name="PasswordOld"
                    rules={[{ required: true, message: 'Vui lòng nhập mật khẩu cũ' }]}>
                    <Input.Password />
                </Form.Item>
                <Form.Item
                    label="Mật khẩu mới"
                    name="PasswordNew"
                    rules={[{ required: true, message: 'Vui lòng nhập mật khẩu mới' }]}>
                    <Input.Password />
                </Form.Item>
                <Form.Item
                    name="confirm"
                    label="Nhập lại mật khẩu mới"
                    dependencies={['PasswordNew']}
                    hasFeedback
                    rules={[
                        {
                            required: true,
                            message: 'Vui lòng nhập lại mật khẩu mới!',
                        },
                        ({ getFieldValue }) => ({
                            validator(_, value) {
                                if (!value || getFieldValue('PasswordNew') === value) {
                                    return Promise.resolve();
                                }
                                return Promise.reject(new Error('Mật khẩu nhập lại không giống!'));
                            },
                        }),
                    ]}>
                    <Input.Password />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        Cập nhật
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default FormChangePassword;
