import { Button, Col, Form, FormProps, Input, message, Row } from 'antd'
import { LockOutlined, UserOutlined, ReloadOutlined, FileProtectOutlined, RobotOutlined } from '@ant-design/icons'
import { useLoginMutation } from '../../redux/auth/Auth.service'
import { LoginRequest } from '../../types/Auth.type'
import { useNavigate } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import { setCredentials } from '../../redux/auth/Auth.slice'
import { loadCaptchaEnginge, LoadCanvasTemplateNoReload, validateCaptcha } from 'react-simple-captcha'
import { useEffect } from 'react'

const LoginPage = () => {
  const [loginapi, { isLoading }] = useLoginMutation()
  const navigate = useNavigate()
  const dispatch = useDispatch()

  useEffect(() => {
    document.title = 'Đăng nhập'
    loadCaptchaEnginge(6)
  }, [])

  const onFinish: FormProps<LoginRequest>['onFinish'] = async (values) => {
    try {
      const userCaptchaInput = document.getElementById('user_captcha_input') as HTMLInputElement
      if (userCaptchaInput) {
        const user_captcha_value: string = userCaptchaInput.value
        if (validateCaptcha(user_captcha_value) == true) {
          const rs: any = await loginapi(values).unwrap()
          message.success(rs.message)
          dispatch(setCredentials({ ...rs }))
          navigate('/')
        } else {
          message.error('Mã captcha không đúng')
        }
      }
    } catch (e: any) {
      console.log(e)
      message.error(e.data?.message ?? 'Đăng nhập thất bại :)')
    }
  }

  return (
    <div className='w-screen h-screen flex items-center justify-center bg-gray-200'>
      <Form
        name='normal_login'
        className='login-form m-auto w-11/12 md:w-4/6 lg:w-5/12 xl:w-2/6 p-3 2xl:p-5 rounded-xl shadow-lg bg-white'
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <h1 className='text-center uppercase mb-3 font-bold text-2xl'>Đăng nhập</h1>
        <Form.Item className='mb-3' name='username' rules={[{ required: true, message: 'Vui lòng nhập tài khoản!' }]}>
          <Input prefix={<UserOutlined className='site-form-item-icon' />} placeholder='Tài khoản' />
        </Form.Item>
        <Form.Item name='password' className='mb-3' rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}>
          <Input.Password prefix={<LockOutlined className='site-form-item-icon' />} placeholder='Mật khẩu' />
        </Form.Item>
        <Form.Item name='code' className='mb-3' rules={[{ required: true, message: 'Vui lòng nhập mã bảo vệ!' }]}>
          <Input
            type='number'
            className='hide-input-arrow'
            prefix={<FileProtectOutlined className='site-form-item-icon' />}
            placeholder='Nhập mã bảo vệ'
          />
        </Form.Item>
        <Form.Item name='captcha' className='mb-3' rules={[{ required: true, message: 'Vui lòng nhập mã captcha!' }]}>
          <Row gutter={{ xs: 8, sm: 16 }}>
            <Col xs={{ span: 24, order: 2 }} md={{ span: 12, order: 1 }}>
              <Input
                prefix={<RobotOutlined className='site-form-item-icon' />}
                id='user_captcha_input'
                placeholder='Nhập mã captcha'
              />
            </Col>
            <Col xs={{ span: 24, order: 1 }} md={{ span: 12, order: 2 }} className='mb-3 md:mb-0'>
              <div className='flex justify-between flex-row-reverse gap-3'>
                <Button className='inline-flex items-center' onClick={() => loadCaptchaEnginge(6)}>
                  <ReloadOutlined />
                </Button>
                <div className='border flex-1 flex justify-center items-center rounded-md '>
                  <LoadCanvasTemplateNoReload />
                </div>
              </div>
            </Col>
          </Row>
        </Form.Item>
        <Form.Item className='mb-0 flex justify-center'>
          <Button type='primary' loading={isLoading} htmlType='submit' className='login-form-button '>
            Đăng nhập
          </Button>
        </Form.Item>
      </Form>
    </div>
  )
}

export default LoginPage
