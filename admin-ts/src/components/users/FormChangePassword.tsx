import { Button, Form, Input, message, Modal } from 'antd'
import { ChangePasswordRequest } from '../../types/Auth.type'
import { useChangePasswordMutation, useLogoutMutation } from '../../redux/auth/Auth.service'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { logout } from '../../redux/auth/Auth.slice'
import { ExclamationCircleFilled } from '@ant-design/icons'
const { confirm } = Modal

const FormChangePassword = () => {
  const [form] = Form.useForm()
  const [changePasswordApi, { isLoading }] = useChangePasswordMutation()
  const [logoutApi] = useLogoutMutation()
  const dispath = useDispatch()
  const navigate = useNavigate()

  const handleLogout = async () => {
    try {
      const rs: any = await logoutApi()
      message.success(rs.data.message)
      dispath(logout())
      navigate('/login')
    } catch (e: any) {
      console.log(e)
      message.error('Đăng xuất tài khoản thất bại :)')
    }
  }

  const showConfirm = () => {
    confirm({
      title: 'Bạn có muốn đăng xuất không?',
      icon: <ExclamationCircleFilled />,
      content: 'Đăng xuất bạn sẽ phải đăng nhập lại với mật khẩu mới :)',
      onOk: async () => {
        console.log('OK')
        await handleLogout()
      },
      onCancel() {
        console.log('Cancel')
      }
    })
  }

  const onFinish = async (values: ChangePasswordRequest) => {
    // console.log('Received values of form: ', values);
    try {
      const rs: any = await changePasswordApi(values).unwrap()
      message.success(rs.message)
      showConfirm()
    } catch (e: any) {
      console.log(e)
      message.error(e.data.message ?? 'Đổi mật khẩu thất bại :)')
    }
  }

  return (
    <div>
      <Form
        layout='vertical'
        form={form}
        style={{ maxWidth: 600 }}
        name='changePassword'
        onFinish={onFinish}
        scrollToFirstError
      >
        <Form.Item
          label='Mật khẩu cũ'
          name='passwordOld'
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu cũ' }]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item
          label='Mật khẩu mới'
          name='passwordNew'
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu mới' }]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item
          name='passwordConfirm'
          label='Nhập lại mật khẩu mới'
          dependencies={['passwordNew']}
          hasFeedback
          rules={[
            {
              required: true,
              message: 'Vui lòng nhập lại mật khẩu mới!'
            },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue('passwordNew') === value) {
                  return Promise.resolve()
                }
                return Promise.reject(new Error('Mật khẩu nhập lại không giống!'))
              }
            })
          ]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item>
          <Button type='primary' loading={isLoading} htmlType='submit'>
            Cập nhật
          </Button>
        </Form.Item>
      </Form>
    </div>
  )
}

export default FormChangePassword
