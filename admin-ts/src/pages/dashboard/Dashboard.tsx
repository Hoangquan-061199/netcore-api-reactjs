import { Card, Col, Row } from 'antd'

const Dashboard = () => {
  document.title = 'Dashboard'
  return (
    <div>
      <div className='app'>
        <h1 className='text-3xl font-bold underline'>Hello world!</h1>
        <Row gutter={16}>
          <Col span={8}>
            <Card title='Card title' bordered={false}>
              Card content
            </Card>
          </Col>
          <Col span={8}>
            <Card title='Card title' bordered={false}>
              Card content
            </Card>
          </Col>
          <Col span={8}>
            <Card title='Card title' bordered={false}>
              Card content
            </Card>
          </Col>
        </Row>
      </div>
    </div>
  )
}

export default Dashboard
