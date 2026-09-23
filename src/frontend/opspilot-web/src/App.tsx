import { useEffect, useState } from 'react'
import './App.css'
import { Layout, Table, Tag, Button, Space, Card, Menu, type TableProps, Typography } from 'antd'

const { Header, Sider, Content } = Layout;
const { Title, Text } = Typography;

type Project = {
  id: number;
  name: string;
  code: string;
  description?: string | null;
  createdAt: string;
  updatedAt: string;
};

function App() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function loadProjects() {
    setLoading(true);
    setError("");

    try {
      const response = await fetch("/api/v1/projects");

      if (!response.ok) {
        throw new Error(`API trả về HTTP ${response.status}`);
      }

      const data: Project[] = await response.json();
      setProjects(data);
    } catch (err) {
      setError(
        err instanceof Error
          ? err.message
          : "Không thể tải danh sách project",
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void loadProjects();
  }, []);

  const columns: TableProps<Project>["columns"] = [
    {
      title: "Tên project",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Mã project",
      dataIndex: "code",
      key: "code",
      render: (code: string) => <Tag color="blue">{code}</Tag>,
    },
    {
      title: "Mô tả",
      dataIndex: "description",
      key: "description",
      render: (value?: string | null) => value || "—",
    },
    {
      title: "Ngày tạo",
      dataIndex: "createdAt",
      key: "createdAt",
      render: (value: string) =>
        new Date(value).toLocaleString(),
    },
  ];

  return (
    <Layout className="app-layout">
      <Sider breakpoint="lg" collapsedWidth="0">
        <div className="brand">OpsPilot</div>

        <Menu
          theme="dark"
          mode="inline"
          selectedKeys={["dashboard"]}
          items={[
            { key: "dashboard", label: "Dashboard" },
            { key: "projects", label: "Projects" },
            { key: "servers", label: "Servers" },
            { key: "applications", label: "Applications" },
            { key: "deployments", label: "Deployments" },
            { key: "alerts", label: "Alerts" },
            { key: "settings", label: "Settings" },
          ]}
        />
      </Sider>

      <Layout>
        <Header className="app-header">
          <Text strong>DevOps Command Center</Text>
          <Text type="secondary">Sprint 1 · Foundation</Text>
        </Header>

        <Content className="app-content">
          <Space direction="vertical" size="large" className="full-width">
            <div>
              <Title level={2}>Dashboard</Title>
              <Text type="secondary">
                Tổng quan quản lý project của OpsPilot.
              </Text>
            </div>

            <Card>
              <Space direction="vertical">
                <Text type="secondary">Tổng số project</Text>
                <Title level={2}>{projects.length}</Title>
              </Space>
            </Card>

            <Card
              title="Projects"
              extra={
                <Button
                  type="primary"
                  onClick={() => void loadProjects()}
                >
                  Làm mới
                </Button>
              }
            >
              {error && (
                <Text type="danger">
                  {error}
                </Text>
              )}

              <Table<Project>
                rowKey="id"
                columns={columns}
                dataSource={projects}
                loading={loading}
                pagination={{ pageSize: 10 }}
                locale={{
                  emptyText: "Chưa có project",
                }}
              />
            </Card>
          </Space>
        </Content>
      </Layout>
    </Layout>
  );
}

export default App
