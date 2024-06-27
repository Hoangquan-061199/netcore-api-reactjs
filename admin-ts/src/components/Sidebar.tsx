import { Menu } from 'antd'
import Sider from 'antd/es/layout/Sider'
import {
  OrderedListOutlined,
  BarChartOutlined,
  UsergroupAddOutlined,
  DashboardOutlined,
  ClusterOutlined,
  UserOutlined,
  SettingOutlined,
  TranslationOutlined,
  ShareAltOutlined,
  LinkOutlined,
  MailOutlined,
  DeleteColumnOutlined,
  LayoutOutlined,
  ApartmentOutlined,
  FileImageOutlined
} from '@ant-design/icons'
import { Link, useLocation } from 'react-router-dom'

type props = {
  collapsed: boolean
}

const Sidebar = ({ collapsed }: props) => {
  const location = useLocation()

  const items = [
    {
      key: 'home',
      type: 'group',
      label: 'Home'
    },
    {
      key: '/',
      icon: <DashboardOutlined />,
      label: <Link to='/'>Dashboard</Link>
    },
    {
      key: 'website',
      type: 'group',
      label: 'Website'
    },
    {
      key: '3',
      icon: <ClusterOutlined />,
      label: 'Sản phẩm',
      children: [
        {
          key: '/module-products',
          label: <Link to='/module-products'>Danh mục sản phẩm</Link>
        },
        {
          key: '/products',
          label: <Link to='/products'>Sản phẩm</Link>
        },
        {
          key: '/attributes',
          label: <Link to='/attributes'>Thuộc tính</Link>
        }
      ]
    },
    {
      key: '4',
      icon: <OrderedListOutlined />,
      label: 'Bài viết',
      children: [
        {
          key: '/module-contents',
          label: <Link to='/module-contents'>Danh mục bài viết</Link>
        },
        {
          key: '/contents',
          label: <Link to='/contents'>Bài viết</Link>
        },
        {
          key: '/other-content',
          label: <Link to='/other-content'>Nội dung khác</Link>
        }
      ]
    },
    {
      key: '/layouts',
      icon: <LayoutOutlined />,
      label: <Link to='/layouts'>Layouts</Link>
    },
    {
      key: '/menus',
      icon: <ApartmentOutlined />,
      label: <Link to='/menus'>Menus</Link>
    },
    {
      key: '/banners',
      icon: <FileImageOutlined />,
      label: <Link to='/banners'>Banners</Link>
    },
    {
      key: 'dev',
      type: 'group',
      label: 'Development'
    },
    {
      key: '/log-admin',
      icon: <BarChartOutlined />,
      label: <Link to='/log-admin'>Log Admin</Link>
    },
    {
      key: 'acc',
      type: 'group',
      label: 'Account'
    },
    {
      key: '/user-admin',
      icon: <UserOutlined />,
      label: <Link to='/user-admin'>Tài khoản</Link>
    },
    {
      key: '/department',
      icon: <UsergroupAddOutlined />,
      label: <Link to='/department'>Phòng ban</Link>
    },
    {
      key: 'system',
      type: 'group',
      label: 'Hệ thống'
    },
    {
      key: '/systems',
      icon: <SettingOutlined />,
      label: <Link to='/systems'>Cấu hình</Link>
    },
    {
      key: '/list-systems',
      icon: <SettingOutlined />,
      label: <Link to='/list-systems'>Danh sách cấu hình</Link>
    },
    {
      key: '/resources',
      icon: <TranslationOutlined />,
      label: <Link to='/resources'>Resources</Link>
    },
    {
      key: '/block-ip',
      icon: <DeleteColumnOutlined />,
      label: <Link to='/block-ip'>Block IP</Link>
    },
    {
      key: '/template-email',
      icon: <MailOutlined />,
      label: <Link to='/template-email'>Template email</Link>
    },
    {
      key: '/redirect-url',
      icon: <LinkOutlined />,
      label: <Link to='/redirect-url'>Redirect Url</Link>
    },
    {
      key: '/sitemap',
      icon: <ShareAltOutlined />,
      label: <Link to='/sitemap'>Sitemap</Link>
    }
  ]
  return (
    <div>
      <Sider
        className='h-full'
        breakpoint='lg'
        // collapsedWidth="0"
        trigger={null}
        collapsible
        collapsed={collapsed}
        // onBreakpoint={(broken) => {
        //     // console.log(broken);
        // }}
        theme='light'
        // onCollapse={(collapsed, type) => {
        //     console.log(collapsed, type);
        // }}
      >
        <div className='demo-logo-vertical flex items-center justify-center gap-3 h-16'>
          <svg xmlns='http://www.w3.org/2000/svg' width='30px' height='30px' viewBox='0 0 200 200' version='1.1'>
            <defs>
              <linearGradient x1='62.1023273%' y1='0%' x2='108.19718%' y2='37.8635764%' id='linearGradient-1'>
                <stop stopColor='#4285EB' offset='0%' />
                <stop stopColor='#2EC7FF' offset='100%' />
              </linearGradient>
              <linearGradient x1='69.644116%' y1='0%' x2='54.0428975%' y2='108.456714%' id='linearGradient-2'>
                <stop stopColor='#29CDFF' offset='0%' />
                <stop stopColor='#148EFF' offset='37.8600687%' />
                <stop stopColor='#0A60FF' offset='100%' />
              </linearGradient>
              <linearGradient
                x1='69.6908165%'
                y1='-12.9743587%'
                x2='16.7228981%'
                y2='117.391248%'
                id='linearGradient-3'
              >
                <stop stopColor='#FA816E' offset='0%' />
                <stop stopColor='#F74A5C' offset='41.472606%' />
                <stop stopColor='#F51D2C' offset='100%' />
              </linearGradient>
              <linearGradient
                x1='68.1279872%'
                y1='-35.6905737%'
                x2='30.4400914%'
                y2='114.942679%'
                id='linearGradient-4'
              >
                <stop stopColor='#FA8E7D' offset='0%' />
                <stop stopColor='#F74A5C' offset='51.2635191%' />
                <stop stopColor='#F51D2C' offset='100%' />
              </linearGradient>
            </defs>
            <g id='Page-1' stroke='none' strokeWidth='1' fill='none' fillRule='evenodd'>
              <g id='logo' transform='translate(-20.000000, -20.000000)'>
                <g id='Group-28-Copy-5' transform='translate(20.000000, 20.000000)'>
                  <g id='Group-27-Copy-3'>
                    <g id='Group-25' fillRule='nonzero'>
                      <g id='2'>
                        <path
                          d='M91.5880863,4.17652823 L4.17996544,91.5127728 C-0.519240605,96.2081146 -0.519240605,103.791885 4.17996544,108.487227 L91.5880863,195.823472 C96.2872923,200.518814 103.877304,200.518814 108.57651,195.823472 L145.225487,159.204632 C149.433969,154.999611 149.433969,148.181924 145.225487,143.976903 C141.017005,139.771881 134.193707,139.771881 129.985225,143.976903 L102.20193,171.737352 C101.032305,172.906015 99.2571609,172.906015 98.0875359,171.737352 L28.285908,101.993122 C27.1162831,100.824459 27.1162831,99.050775 28.285908,97.8821118 L98.0875359,28.1378823 C99.2571609,26.9692191 101.032305,26.9692191 102.20193,28.1378823 L129.985225,55.8983314 C134.193707,60.1033528 141.017005,60.1033528 145.225487,55.8983314 C149.433969,51.69331 149.433969,44.8756232 145.225487,40.6706018 L108.58055,4.05574592 C103.862049,-0.537986846 96.2692618,-0.500797906 91.5880863,4.17652823 Z'
                          id='Shape'
                          fill='url(#linearGradient-1)'
                        />
                        <path
                          d='M91.5880863,4.17652823 L4.17996544,91.5127728 C-0.519240605,96.2081146 -0.519240605,103.791885 4.17996544,108.487227 L91.5880863,195.823472 C96.2872923,200.518814 103.877304,200.518814 108.57651,195.823472 L145.225487,159.204632 C149.433969,154.999611 149.433969,148.181924 145.225487,143.976903 C141.017005,139.771881 134.193707,139.771881 129.985225,143.976903 L102.20193,171.737352 C101.032305,172.906015 99.2571609,172.906015 98.0875359,171.737352 L28.285908,101.993122 C27.1162831,100.824459 27.1162831,99.050775 28.285908,97.8821118 L98.0875359,28.1378823 C100.999864,25.6271836 105.751642,20.541824 112.729652,19.3524487 C117.915585,18.4685261 123.585219,20.4140239 129.738554,25.1889424 C125.624663,21.0784292 118.571995,14.0340304 108.58055,4.05574592 C103.862049,-0.537986846 96.2692618,-0.500797906 91.5880863,4.17652823 Z'
                          id='Shape'
                          fill='url(#linearGradient-2)'
                        />
                      </g>
                      <path
                        d='M153.685633,135.854579 C157.894115,140.0596 164.717412,140.0596 168.925894,135.854579 L195.959977,108.842726 C200.659183,104.147384 200.659183,96.5636133 195.960527,91.8688194 L168.690777,64.7181159 C164.472332,60.5180858 157.646868,60.5241425 153.435895,64.7316526 C149.227413,68.936674 149.227413,75.7543607 153.435895,79.9593821 L171.854035,98.3623765 C173.02366,99.5310396 173.02366,101.304724 171.854035,102.473387 L153.685633,120.626849 C149.47715,124.83187 149.47715,131.649557 153.685633,135.854579 Z'
                        id='Shape'
                        fill='url(#linearGradient-3)'
                      />
                    </g>
                    <ellipse
                      id='Combined-Shape'
                      fill='url(#linearGradient-4)'
                      cx='100.519339'
                      cy='100.436681'
                      rx='23.6001926'
                      ry='23.580786'
                    />
                  </g>
                </g>
              </g>
            </g>
          </svg>
          {!collapsed && <p>Ant Design</p>}
        </div>
        <Menu
          theme='light'
          mode='inline'
          className='text-xs overflow-y-auto'
          style={{ maxHeight: 'calc(100vh - 64px)' }}
          defaultSelectedKeys={[location.pathname]}
          items={items}
        />
      </Sider>
    </div>
  )
}

export default Sidebar
