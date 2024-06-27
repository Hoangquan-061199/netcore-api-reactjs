import { Checkbox, Space, Table, Tag } from 'antd'
import type { CheckboxProps, TableProps } from 'antd'

interface DataType {
  key: string
  name: string
  isAdd: boolean
}

const columns: TableProps<DataType>['columns'] = [
  {
    title: 'Danh sách hệ thống',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: 'Thêm',
    dataIndex: 'isAdd',
    key: 'isAdd',
    render: (record: DataType) => {
      const onChange: CheckboxProps['onChange'] = (e) => {
        console.log(`checked = ${e.target.checked}`)
      }
      return (
        <>
          <Checkbox onChange={onChange} defaultChecked={record.isAdd}>
            Checkbox
          </Checkbox>
        </>
      )
    }
  }
]

const data: DataType[] = [
  {
    key: '1',
    name: 'Danh mục sản phẩm',
    isAdd: true
  }
]
const RolesPermission = () => {
  return (
    <>
      <Table columns={columns} dataSource={data} />
    </>
  )
}

export default RolesPermission
