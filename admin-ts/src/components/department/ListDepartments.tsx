import { Button, Dropdown, MenuProps, message, Popconfirm, PopconfirmProps, Space, Table, TableProps, Tag } from 'antd'
import { CheckOutlined, StopOutlined, DeleteOutlined, EditOutlined } from '@ant-design/icons'

interface DataType {
  key: string
  name: string
  age: number
  address: string
  tags: string[]
}

const confirm: PopconfirmProps['onConfirm'] = (e) => {
  console.log(e)
  message.success('Click on Yes')
}

const cancel: PopconfirmProps['onCancel'] = (e) => {
  console.log(e)
  message.error('Click on No')
}

const ListDepartments = () => {
  const columns: TableProps<DataType>['columns'] = [
    {
      title: 'Tên phòng ban',
      dataIndex: 'name',
      key: 'name'
    },
    {
      title: 'Trạng thái',
      align: 'center',
      key: 'status',
      width: 200,
      render: () => {
        return (
          <div>
            <Tag color={true ? 'processing' : 'default'}>{true ? 'hiện' : 'ẩn'}</Tag>
          </div>
        )
      }
    },
    {
      title: 'Action',
      key: 'action',
      width: 200,
      align: 'center',
      render: () => {
        return (
          <Space>
            <Button
              size='small'
              // onClick={() =>
              //     showModal({
              //         type: 'edit',
              //         title: 'Sửa tài khoản',
              //         width: 800
              //     })
              // }
              type='primary'
            >
              <EditOutlined />
            </Button>

            <Popconfirm
              title='Xoá tài khoản'
              description='Bạn có chắc muốn xoá tài khoản này?'
              onConfirm={confirm}
              onCancel={cancel}
              okText='Yes'
              cancelText='No'
            >
              <Button size='small' danger={true}>
                <DeleteOutlined />
              </Button>
            </Popconfirm>
          </Space>
        )
      }
    }
  ]

  const items: MenuProps['items'] = [
    {
      key: '1',
      label: 'Ẩn phòng ban',
      icon: <StopOutlined />
    },
    {
      key: '2',
      label: 'Hiện phòng ban',
      icon: <CheckOutlined />
    },
    {
      key: '3',
      label: 'Xoá phòng ban',
      icon: <DeleteOutlined />
    }
  ]

  const data: DataType[] = [
    {
      key: '1',
      name: 'John Brown',
      age: 32,
      address: 'New York No. 1 Lake Park',
      tags: ['nice', 'developer']
    },
    {
      key: '2',
      name: 'Jim Green',
      age: 42,
      address: 'London No. 1 Lake Park',
      tags: ['loser']
    },
    {
      key: '3',
      name: 'Joe Black',
      age: 32,
      address: 'Sydney No. 1 Lake Park',
      tags: ['cool', 'teacher']
    }
  ]
  return (
    <div style={{ padding: 15 }}>
      <Space className='header-table' style={{ paddingBottom: 15 }}>
        <Button
          type='primary'
          // onClick={() =>
          //     showModal({
          //         type: 'add',
          //         title: 'Thêm mới tài khoản',
          //         width: 800,
          //     })
          // }
          style={{ backgroundColor: '#52c41a' }}
        >
          Thêm mới
        </Button>
        <Dropdown menu={{ items }} placement='bottomLeft' arrow>
          <Button type='default'>Hành động</Button>
        </Dropdown>
      </Space>
      <Table columns={columns} dataSource={data} />
    </div>
  )
}

export default ListDepartments
