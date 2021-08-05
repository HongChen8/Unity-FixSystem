
using System.Collections.Generic;
using TrueSync;
namespace FixSystem
{

    public class PolygonCollider : BaseCollider
    {
        public List<TSVector2> vertexs = new List<TSVector2>();
        public PolygonCollider(Entity entity) : base(entity)
        {

        }
        public override IShape GetShape()
        {
            return new OBBShape(GetTrueVertexs());
        }
        public override Rectangle GetRectangle()
        {
            return new Rectangle(GetTrueVertexs());
        }



        /// <summary>
        /// 获取实际的顶点坐标
        /// </summary>
        /// <returns></returns>
        public List<TSVector2> GetTrueVertexs()
        {
            List<TSVector2> vertexs = new List<TSVector2>();
            foreach (var item in this.vertexs)
            {
                vertexs.Add(GetTransformationVector(item, LocalScale, Angle, Position));
            }
            return vertexs;
        }

        /// <summary>
        /// 变换向量
        /// 1.计算缩放
        /// 2.计算旋转
        /// 3.计算偏移
        /// </summary>
        /// <param name="target">目标向量</param>
        /// <param name="localScale">缩放系数</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="center">旋转中心</param>
        /// <returns></returns>
        private TSVector2 GetTransformationVector(TSVector2 target, TSVector2 localScale, FP angle, TSVector2 center)
        {
            // 计算缩放后的坐标
            target.x = target.x * localScale.x;
            target.y = target.y * localScale.y;
            // 计算旋转后的坐标
            angle = angle / TSMath.Rad2Deg;
            var x = target.x * TSMath.Cos(angle) - target.y * TSMath.Sin(angle);
            var y = target.x * TSMath.Sin(angle) + target.y * TSMath.Cos(angle);
            // 计算偏移后的坐标
            return new TSVector2(x, y) + center;
        }

        private void Reset()
        {
            // vertexs.Clear();
            // if (SpriteRenderer != null)
            // {
            //     var width = SpriteRenderer.size.x;
            //     var height = SpriteRenderer.size.y;
            //     vertexs.Add(new TSVector2(-width / 2, -height / 2));
            //     vertexs.Add(new TSVector2(-width / 2, height / 2));
            //     vertexs.Add(new TSVector2(width / 2, height / 2));
            //     vertexs.Add(new TSVector2(width / 2, -height / 2));
            // }
            // else
            // {
            //     vertexs.Add(new TSVector2(-1, -1));
            //     vertexs.Add(new TSVector2(-1, 1));
            //     vertexs.Add(new TSVector2(1, 1));
            //     vertexs.Add(new TSVector2(1, -1));
            // }
        }
    }
}