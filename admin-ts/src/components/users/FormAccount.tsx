import { Button, Col, Form, Input, Row, Upload, message } from 'antd';
import { useGetUserUpdateQuery, useUpdateAccountLoginMutation } from '../../redux/users/user.service';
import { useEffect, useState } from 'react';
import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import type { GetProp, UploadProps } from 'antd';
import { UpdateAccountRequest } from '../../types/User.type';
import { useDispatch, useSelector } from 'react-redux';
import { selectCurrentUserHeader, UpdateAccountLogin, userHeader } from '../../redux/users/user.slice';
import { getImageDomain } from '../../helpers/utilities';

type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

const getBase64 = (img: FileType, callback: (url: string) => void) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result as string));
    reader.readAsDataURL(img);
};

const FormAccount = () => {
    const [form] = Form.useForm();
    const { data } = useGetUserUpdateQuery();
    const [updateAccLoginApi, { isLoading }] = useUpdateAccountLoginMutation();
    const [loading, setLoading] = useState(false);
    const [imageUrl, setImageUrl] = useState<string>();
    const [file, setFile] = useState<FileType>();
    const dispatch = useDispatch();
    const user = useSelector(selectCurrentUserHeader)
    const onFinish = async (values: any) => {
        let req: UpdateAccountRequest = {
            fullName: values.fullName,
            email: values.email,
            file: file,
        };

        try {
            let rs: any = await updateAccLoginApi(req).unwrap();
            message.success(rs.message);
            dispatch(UpdateAccountLogin({ ...rs.data }));
            dispatch(
                userHeader({
                    ...user,
                    fullName: rs.data.fullName,
                    urlPicture: rs.data.urlPicture
                })
            );
        } catch (e: any) {
            console.log(e);
            message.error(e.data.message ?? 'Cập nhật thất bại :)');
        }
    };

    useEffect(() => {
        if (data) {
            const { fullName, email, userName, departmentName, roles, createdDate, urlPicture } = data;
            form.setFieldsValue({ fullName, email, userName, departmentName, roles, createdDate, urlPicture });
        }
    }, [data, form]);

    useEffect(() => {
        let url = getImageDomain(data?.urlPicture, null);
        setImageUrl(url);
    }, [data?.urlPicture]);

    const beforeUpload = (file: FileType) => {
        setLoading(true);
        const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png' || file.type === 'image/webp';
        if (!isJpgOrPng) {
            message.error('Chỉ hỗ trợ file JPG/PNG/WEBP <3');
            return false;
        }
        const isLt1M = file.size / 1024 / 1024 < 1;
        if (!isLt1M) {
            message.error('file không được lớn hơn 1mb');
            return false;
        }
        getBase64(file, (url) => {
            setLoading(false);
            setImageUrl(url);
            setFile(file);
        });
        // Prevent upload
        return false;
    };

    const uploadButton = (
        <button style={{ border: 0, background: 'none' }} type="button">
            {loading ? <LoadingOutlined /> : <PlusOutlined />}
            <div style={{ marginTop: 8 }}>Upload</div>
        </button>
    );

    return (
        <div>
            <Form layout="vertical" form={form} name="updateaccount" onFinish={onFinish} scrollToFirstError>
                <Row gutter={16}>
                    <Col className="gutter-row" span={4}>
                        <div style={{ height: 'auto', width: '80%', aspectRatio: 1 }}>
                            <Form.Item name="urlPicture">
                                <Upload
                                    name="file"
                                    listType="picture-circle"
                                    className="avatar-uploader"
                                    showUploadList={false}
                                    beforeUpload={beforeUpload}
                                    fileList={[]}
                                    maxCount={1}>
                                    {imageUrl ? (
                                        <img
                                            src={imageUrl}
                                            alt="avatar"
                                            style={{ width: '100%', aspectRatio: 1, borderRadius: '50%' }}
                                        />
                                    ) : (
                                        uploadButton
                                    )}
                                </Upload>
                            </Form.Item>
                        </div>
                    </Col>
                    <Col className="gutter-row" span={20}>
                        <Row gutter={16}>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="userName" label="Tài khoản:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="departmentName" label="Phòng ban:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="roles" label="Quyền:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item name="createdDate" label="Ngày tạo:">
                                    <Input disabled={true} />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item
                                    name="fullName"
                                    label="Họ tên:"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'Vui lòng nhập họ tên',
                                        },
                                    ]}>
                                    <Input />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item
                                    name="email"
                                    label="Email:"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'Vui lòng nhập Email',
                                        },
                                        {
                                            type: 'email',
                                            message: 'Email không hợp lệ',
                                        },
                                    ]}>
                                    <Input />
                                </Form.Item>
                            </Col>
                            <Col className="gutter-row" span={12}>
                                <Form.Item>
                                    <Button type="primary" loading={isLoading} htmlType="submit">
                                        Cập nhật
                                    </Button>
                                </Form.Item>
                            </Col>
                        </Row>
                    </Col>
                </Row>
            </Form>
        </div>
    );
};

export default FormAccount;
