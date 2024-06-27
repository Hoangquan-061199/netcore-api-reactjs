import { Footer } from 'antd/es/layout/layout'

const FooterComponent = () => {
  return (
    <div>
      <Footer style={{ textAlign: 'center' }}>Ant Design ©{new Date().getFullYear()} Created by Ant UED</Footer>
    </div>
  )
}

export default FooterComponent
