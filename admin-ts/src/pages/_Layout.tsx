import { useSelector } from 'react-redux'
import { Navigate, Outlet } from 'react-router-dom'
import { selectCurrentToken } from '../redux/auth/Auth.slice'
import { Layout, theme } from 'antd'
import Sidebar from '../components/Sidebar'
import { useEffect, useMemo, useRef, useState } from 'react'
import HeaderComponent from '../components/Header'
import FooterComponent from '../components/Footer'
import { getInnerHeight } from '../helpers/utilities'

const { Content } = Layout

const _Layout = () => {
  const istoken = useSelector(selectCurrentToken)
  const [collapsed, setCollapsed] = useState(false)
  const {
    token: { colorBgContainer }
  } = theme.useToken()

  return istoken ? (
    <>
      <Layout className='h-screen overflow-hidden'>
        <Sidebar collapsed={collapsed} />
        <Layout>
          <HeaderComponent collapsed={collapsed} colorBgContainer={colorBgContainer} setCollapsed={setCollapsed} />
          <Content
            className='mt-6 mb-0 mx-4'
            style={{
              background: colorBgContainer,
              height: 'calc(100vh - 64px - 69px - 24px)'
            }}
          >
            <div className='h-full p-4' style={{maxHeight: '100%'}}>
              <Outlet />
            </div>
          </Content>
          <FooterComponent />
        </Layout>
      </Layout>
    </>
  ) : (
    <Navigate to='/login' replace={true} />
  )
}

export default _Layout
