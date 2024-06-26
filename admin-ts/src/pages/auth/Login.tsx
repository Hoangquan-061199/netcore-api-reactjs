import { Button, Col, Form, FormProps, Input, message, Row } from 'antd';
import { LockOutlined, UserOutlined, ReloadOutlined, FileProtectOutlined } from '@ant-design/icons';
import { useLoginMutation } from '../../redux/auth/Auth.service';
import { LoginRequest } from '../../types/Auth.type';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../redux/auth/Auth.slice';
import { loadCaptchaEnginge, LoadCanvasTemplateNoReload, validateCaptcha } from 'react-simple-captcha';
import { useEffect } from 'react';

const LoginPage = () => {
    const [loginapi, { isLoading }] = useLoginMutation();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        document.title = 'Đăng nhập';
        loadCaptchaEnginge(6);
    }, []);

    const onFinish: FormProps<LoginRequest>['onFinish'] = async (values) => {
        try {
            const userCaptchaInput = document.getElementById('user_captcha_input') as HTMLInputElement;
            if (userCaptchaInput) {
                const user_captcha_value: string = userCaptchaInput.value;
                if (validateCaptcha(user_captcha_value) == true) {
                    let rs: any = await loginapi(values).unwrap();
                    message.success(rs.message);
                    dispatch(setCredentials({ ...rs }));
                    navigate('/');
                } else {
                    message.error('Mã captcha không đúng');
                }
            }
        } catch (e: any) {
            console.log(e);
            message.error(e.data?.message ?? 'Đăng nhập thất bại :)');
        }
    };

    return (
        <div style={{ width: '100vw', height: '100vh', display: 'flex', backgroundColor: '#f0f0f0' }}>
            <Form
                name="normal_login"
                style={{
                    margin: 'auto',
                    width: 500,
                    padding: 20,
                    borderRadius: 10,
                    boxShadow: '0 0 10px rgba(0,0,0,0.1)',
                    backgroundColor: '#fff',
                }}
                className="login-form"
                initialValues={{ remember: true }}
                onFinish={onFinish}>
                <h1 style={{ textAlign: 'center', textTransform: 'uppercase', margin: 15 }}>Đăng nhập</h1>
                <Form.Item name="username" rules={[{ required: true, message: 'Vui lòng nhập tài khoản!' }]}>
                    <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="Tài khoản" />
                </Form.Item>
                <Form.Item name="password" rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}>
                    <Input.Password prefix={<LockOutlined className="site-form-item-icon" />} placeholder="Mật khẩu" />
                </Form.Item>
                <Form.Item name="code" rules={[{ required: true, message: 'Vui lòng nhập mã bảo vệ!' }]}>
                    <Input type='number' className="hide-input-arrow" prefix={<FileProtectOutlined className="site-form-item-icon" />} placeholder="Nhập mã bảo vệ" />
                </Form.Item>
                <Form.Item name="captcha" rules={[{ required: true, message: 'Vui lòng nhập mã captcha!' }]}>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Input id="user_captcha_input" placeholder="Nhập mã captcha" />
                        </Col>
                        <Col span={12}>
                            <Row gutter={16}>
                                <Col span={6}>
                                    <Button onClick={() => loadCaptchaEnginge(6)}>
                                        <ReloadOutlined />
                                    </Button>
                                </Col>
                                <Col span={18}>
                                    <LoadCanvasTemplateNoReload />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                </Form.Item>
                <Form.Item style={{ display: 'flex', justifyContent: 'center' }}>
                    <Button type="primary" loading={isLoading} htmlType="submit" className="login-form-button">
                        Đăng nhập
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default LoginPage;
