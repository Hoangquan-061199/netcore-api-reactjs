import { Button, message, Modal } from 'antd'
import { ExclamationCircleFilled } from '@ant-design/icons'
import { useDeleteAccountMutation, useLogoutMutation } from '../../redux/auth/Auth.service'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { logout } from '../../redux/auth/Auth.slice'

const { confirm } = Modal

const DeleteAccount = () => {
  const [deleteAccApi, { isLoading }] = useDeleteAccountMutation()
  const [logoutApi] = useLogoutMutation()
  const dispath = useDispatch()
  const navigate = useNavigate()

  const handleLogout = async () => {
    try {
      const logoutrs: any = await logoutApi()
      message.success(logoutrs.data.message)
      dispath(logout())
      navigate('/login')
    } catch (e: any) {
      console.log(e)
      message.error('Đăng xuất tài khoản thất bại :)')
    }
  }

  const showConfirm = () => {
    confirm({
      title: 'Bạn chắc chắn muốn xoá tài khoản chứ',
      icon: <ExclamationCircleFilled />,
      content: 'Xoá tài khoản bạn sẽ không thể đăng nhập vào trang quản trị :(',
      onOk: async () => {
        console.log('OK')
        try {
          const rs: any = await deleteAccApi()
          message.success(rs.data.message)
          await handleLogout()
        } catch (e: any) {
          console.log(e)
          message.error(e.data.message ?? 'Xoá tài khoản thất bại')
        }
      },
      onCancel() {
        console.log('Cancel')
      }
    })
  }
  return (
    <>
      <Button onClick={showConfirm} loading={isLoading} danger>
        Xoá tài khoản
      </Button>
    </>
  )
}

export default DeleteAccount
