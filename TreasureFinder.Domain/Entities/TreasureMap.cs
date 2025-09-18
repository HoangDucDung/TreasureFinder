namespace TreasureFinder.Domain.Entities
{
    public class TreasureMap
    {
        public Guid Id { get; set; }
        public int NumRows { get; set; }
        public int NumCols { get; set; }
        public int NumChestTypes { get; set; }
        public double MinimumFuel { get; set; }
        public string OptimalPath { get; set; } = string.Empty;
        public ICollection<Island> Islands { get; set; } = new List<Island>();

        /// <summary>
        /// Handle tính toán đường đi tối ưu và nhiên liệu tối thiểu
        /// </summary>
        /// <param name="map"></param>
        public void CalculateTreasureRoute(TreasureMap map)
        {
            // Vị trí bắt đầu
            var startPosition = (row: 1, col: 1);

            // Tạo dictionary lưu vị trí của các rương
            var chestPositions = new Dictionary<int, List<(int row, int col)>>();

            // Phân loại các đảo theo số rương
            foreach (var island in map.Islands)
            {
                if (!chestPositions.ContainsKey(island.ChestNumber))
                    chestPositions[island.ChestNumber] = new List<(int, int)>();

                chestPositions[island.ChestNumber].Add((island.RowPosition, island.ColPosition));
            }

            // Kiểm tra xem có đủ rương từ 1 đến p không
            for (int i = 1; i <= map.NumChestTypes; i++)
            {
                if (!chestPositions.ContainsKey(i))
                    MinimumFuel = 0;
                    OptimalPath = $"Không tìm thấy rương số {i}";
            }

            // Cấu trúc để lưu trữ trạng thái tối ưu
            // key: (currentKey, row, col) - đã có chìa khóa currentKey và đang ở vị trí (row, col)
            // value: lượng nhiên liệu tối thiểu
            var dp = new Dictionary<(int key, int row, int col), double>();

            // Lưu trữ đường đi tối ưu
            var prev = new Dictionary<(int key, int row, int col), (int key, int row, int col)?>();

            // Trạng thái ban đầu
            dp[(0, startPosition.row, startPosition.col)] = 0;
            prev[(0, startPosition.row, startPosition.col)] = null;

            // Queue cho BFS/Dijkstra
            var pq = new List<((int key, int row, int col) state, double fuel)>
            {
                ((0, startPosition.row, startPosition.col), 0)
            };

            // Trạng thái đích
            (int key, int row, int col)? finalState = null;

            // Dijkstra algorithm
            while (pq.Count > 0)
            {
                // Lấy trạng thái với nhiên liệu ít nhất
                pq.Sort((a, b) => a.fuel.CompareTo(b.fuel));
                var current = pq[0];
                pq.RemoveAt(0);

                var (currentState, currentFuel) = current;
                var (currentKey, currentRow, currentCol) = currentState;

                // Nếu đã tìm thấy kho báu
                if (currentKey == map.NumChestTypes)
                {
                    finalState = currentState;
                    break;
                }

                // Nếu đã có trạng thái tốt hơn, bỏ qua
                if (dp.ContainsKey(currentState) && dp[currentState] < currentFuel)
                    continue;

                // Xem xét tất cả các rương tiếp theo
                int nextKey = currentKey + 1;
                if (chestPositions.ContainsKey(nextKey))
                {
                    foreach (var (nextRow, nextCol) in chestPositions[nextKey])
                    {
                        // Tính nhiên liệu cần thiết
                        double fuelNeeded = CalculateDistance(currentRow, currentCol, nextRow, nextCol);
                        double totalFuel = currentFuel + fuelNeeded;

                        // Trạng thái mới
                        var nextState = (nextKey, nextRow, nextCol);

                        // Nếu tìm thấy đường đi tốt hơn
                        if (!dp.ContainsKey(nextState) || totalFuel < dp[nextState])
                        {
                            dp[nextState] = totalFuel;
                            prev[nextState] = currentState;
                            pq.Add((nextState, totalFuel));
                        }
                    }
                }
            }

            // Nếu không tìm thấy đường đi
            if (finalState == null)
            {
                MinimumFuel = 0;
                OptimalPath = "Không tìm thấy đường đi đến kho báu";
            }

            // Khôi phục đường đi
            var path = new List<(int key, int row, int col)>();
            var state = finalState.Value;

            while (prev.ContainsKey(state) && prev[state].HasValue)
            {
                path.Add(state);
                state = prev[state].Value;
            }

            path.Add((0, startPosition.row, startPosition.col));
            path.Reverse();

            // Tạo mô tả đường đi
            var pathDescription = path.Select(s => $"({s.row},{s.col})").ToList();

            MinimumFuel = dp[finalState.Value];
            OptimalPath = string.Join(" -> ", pathDescription);
        }

        /// <summary>
        ///  Công thức tính nhiên liệu
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
