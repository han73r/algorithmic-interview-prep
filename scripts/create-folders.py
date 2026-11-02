
from pathlib import Path
import re

# Корневая папка проекта
ROOT = Path(__file__).resolve().parent.parent

# Источник ссылок
LINKS_FILE = ROOT / "docs" / "links.txt"

# Папки для создания
TARGET_DIRS = [
    ROOT / "problems",
    ROOT / "solutions",
]

# Читаем ссылки
links = LINKS_FILE.read_text(encoding="utf-8").splitlines()

# Извлекаем названия задач
names = set()

for line in links:
    match = re.search(r"leetcode\.com/problems/([^/]+)", line)
    if match:
        names.add(match.group(1))

# Создаём папки и файлы
created = 0

for name in names:
    for directory in TARGET_DIRS:
        folder = directory / name

        if not folder.exists():
            folder.mkdir(parents=True, exist_ok=True)
            created += 1

    # Создаём JSON внутри problems/<name>/
    json_file = ROOT / "problems" / name / f"{name}.json"

    if not json_file.exists():
        json_file.write_text("{}\n", encoding="utf-8")
        created += 1

print(f"Created {created} new folders/files.")
