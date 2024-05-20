import { Button, Dropdown, MenuProps, Space, Table, TableColumnsType, TableProps, Tooltip } from 'antd';
import { useState } from 'react';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';

interface DataType {
    id: React.Key;
    action: string;
    link: string;
    createdDate: string;
    content: string;
    user: string;
}

const items: MenuProps['items'] = [
    {
        key: '1',
        label: 'Xoá tất cả',
        icon: <DeleteOutlined />,
    },
];

const LogsAdmin = () => {
    const [loading, setLoading] = useState(false);

    const columns: TableColumnsType<DataType> = [
        {
            title: 'ID',
            dataIndex: 'id',
        },
        {
            title: 'Thao tác',
            dataIndex: 'action',
        },
        {
            title: 'Người dùng',
            dataIndex: 'user',
        },
        {
            title: 'Link',
            dataIndex: 'link',
        },
        {
            title: 'Ngày tạo',
            dataIndex: 'createdDate',
        },
        { title: 'Nội dung', dataIndex: 'content' },
        {
            title: (
                <Dropdown menu={{ items }} placement="bottom">
                    <Button>Hành động</Button>
                </Dropdown>
            ),
            dataIndex: 'actiontype',
            align: 'center',
        },
    ];

    const data = [
        {
            id: 1,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 2,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 3,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 4,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 6,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
            actiontype: (
                <Space>
                    <Tooltip title="Xoá log">
                        <DeleteOutlined style={{ color: 'red', cursor: 'pointer' }} />
                    </Tooltip>
                </Space>
            ),
        },
    ];

    const onChange: TableProps<DataType>['onChange'] = (pagination, filters, sorter, extra) => {
        console.log('params', pagination, filters, sorter, extra);
    };

    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

    const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
    };

    return (
        <div>
            <Table
                rowSelection={rowSelection}
                columns={columns}
                dataSource={data ?? []}
                onChange={onChange}
                loading={loading}
                // scroll={{ y: 500 }}
                showSorterTooltip={{ target: 'sorter-icon' }}
            />
        </div>
    );
};

export default LogsAdmin;
