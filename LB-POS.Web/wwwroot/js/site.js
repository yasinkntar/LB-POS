@section Scripts {
    <script>
        document.getElementById('claimsSearch').addEventListener('keyup', function() {
            let filter = this.value.toLowerCase();
        let rows = document.querySelectorAll('.claim-row');
        let visibleCount = 0;
        
            rows.forEach(row => {
            let text = row.querySelector('.claim-name').textContent.toLowerCase();
        if (text.includes(filter)) {
            row.style.display = "";
        visibleCount++;
                } else {
            row.style.display = "none";
                }
            });

        // تحديث عداد الصلاحيات الظاهرة
        document.getElementById('claimsCount').innerHTML =
        `<i class="fas fa-search me-1 ms-1"></i> تم العثور على: ${visibleCount}`;
        });
    </script>
}